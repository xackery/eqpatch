using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace EQPatch
{
    public partial class Main : Form
    {
        List<FileEntry> downloadList = new List<FileEntry>();
        private Dictionary<string, string> configDictionary;
        private ClientType clientType;
        private int totalCountOfFilesToDownload;
        private int totalCountOfBytesToDownload;

        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public Main()
        {
            InitializeComponent();
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(configDictionary["website"] + "?client_id=" + (int)clientType);
                lblProgress.Text = "Getting list of files to patch...";
                string jsonData = GetSimpleWebData(configDictionary["website"] + "?client_id=" + (int)clientType);
                Console.WriteLine(jsonData);

                lblProgress.Text = "Analyzing local files needing to be patched...";
                FileEntry[] fileList = Serializer.DeserializeFromString<FileEntry[]>(jsonData);
                string fullPath;
                downloadList.Clear();
                foreach (FileEntry fileEntry in fileList)
                {
                    fullPath = baseDirectory + fileEntry.pathName + "\\" + fileEntry.fileName;
                    if (File.Exists(fullPath))
                    {
                        if (!Serializer.CompareFileToMD5(fullPath, fileEntry.md5)) downloadList.Add(fileEntry);
                        else Console.WriteLine("Skipping " + fullPath);
                    }
                    else downloadList.Add(fileEntry);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "("+exception.HelpLink+")", "Error during pre-patch process");
            }
            
           
                PatchFiles();
                try
                {
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "(" + exception.HelpLink + ")", "Error during patch process");
            }
        }

        private void PatchFiles()
        {
            lblProgress.Text = "Starting to patch files...";
            totalCountOfBytesToDownload = 0;
            totalCountOfFilesToDownload = 0;
            foreach (FileEntry fileEntry in downloadList)
            {
                totalCountOfFilesToDownload++;
                totalCountOfBytesToDownload += fileEntry.byteSize;
            }
            progressBar.Maximum = totalCountOfFilesToDownload;
            progressBar.Value = 0;
            foreach (FileEntry fileEntry in downloadList)
            {
                PatchFile(fileEntry);
            }
            lblProgress.Text = "Done patching.";
            progressBar.Value = progressBar.Maximum;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadPatchDirectoryList();
            try
            {
                string iniPath = baseDirectory + "\\eqpatch.ini";
                if (!File.Exists(iniPath)) throw new Exception("File not found: eqpatch.ini");
                string content = File.ReadAllText(iniPath);
                configDictionary = Serializer.DeserializeFromString<Dictionary<string, string>>(content);

                lblProgress.Text = "Determining Client..";
                DetermineClientType();
                if (configDictionary.ContainsKey("title")) this.Text = configDictionary["title"] + " - " + clientType.ToString();
                else this.Text = clientType.ToString();
                lblProgress.Text = "Ready to patch.";

                /*if (File.Exists(baseDirectory+"skin.jpg")) {
                    Console.WriteLine("Loading skin");
                    this.BackgroundImage = Image.FromFile(baseDirectory + "skin.jpg");
                }*/
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "An error occured during startup.");
                Application.Exit();
            }
        }

        private void LoadPatchDirectoryList()
        {
            string[] filePaths = Directory.GetDirectories(baseDirectory+"patch");
            foreach (string file in filePaths) {
                string newFile = file.Replace(baseDirectory+"patch\\", "");
                cmbPatchList.Items.Add(newFile);
            }
        }

        private void PatchFile(FileEntry fileEntry)
        {
            BackupFile(fileEntry);
            Console.WriteLine("Patch " + fileEntry.fileName);
            GetSimpleWebFile(fileEntry);
        }

        private void BackupFile(FileEntry fileEntry)
        {
            string fullSourcePath = baseDirectory + "\\" + fileEntry.pathName + "\\";
            string fullDestinationPath = baseDirectory + "\\backup\\" + fileEntry.pathName + "\\";
            if (!File.Exists(fullSourcePath + fileEntry.fileName)) return;
            lblProgress.Text = "Backing up old copy of " + fileEntry.fileName + "...";
            if (!Directory.Exists(baseDirectory + "\\backup\\")) Directory.CreateDirectory(baseDirectory + "\\backup\\");
            
            
            if (!Directory.Exists(fullDestinationPath)) Directory.CreateDirectory(fullDestinationPath);
            File.Copy(fullSourcePath + fileEntry.fileName, fullDestinationPath + fileEntry.fileName);
            Console.WriteLine("Backing up " + fullSourcePath + fileEntry.fileName + " to " + fullDestinationPath + fileEntry.fileName);
        }

        private string GetSimpleWebData(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        private void GetSimpleWebFile(FileEntry fileEntry)
        {
            progressBar.Value++;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            string fullPath = baseDirectory + fileEntry.pathName + "\\";
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
            webClient.DownloadFileAsync(new Uri(fileEntry.webPath), fullPath+fileEntry.fileName);
            Console.WriteLine("Downloading " + fullPath + fileEntry.fileName+ "via URL: "+fileEntry.webPath);
            lblProgress.Text = "Downloading " + fileEntry.fileName + "...";
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null) MessageBox.Show(e.Error.Message);
        }

        private void DetermineClientType()
        {
            string filePath = baseDirectory + "\\eqgame.exe";
            if (!File.Exists(filePath)) throw new Exception("Run this patcher in your base EverQuest directory");
            if (Serializer.CompareFileToMD5(filePath, "859e89987aa636d36b1007f11c2cd6e0")) clientType = ClientType.Underfoot;
            else if (Serializer.CompareFileToMD5(filePath, "85218fc053d8b367f2b704bac5e30acc")) clientType = ClientType.SoF;
            else if (Serializer.CompareFileToMD5(filePath, "bb42bc3870f59b6424a56fed3289c6d4")) clientType = ClientType.Titanium;
            else if (Serializer.CompareFileToMD5(filePath, "")) clientType = ClientType.RoF;
            else throw new Exception("Your eqgame.exe is an unknown version.\n Please go to eqemulator.net and private message Shin Noir your EverQuest client version and MD5: " + Serializer.GetMD5HashFromFile(filePath));
        }
    }

    public enum ClientType
    {
        Titanium = 0,
        SoF = 1,
        Underfoot = 2,
        RoF = 3,
    }
}
