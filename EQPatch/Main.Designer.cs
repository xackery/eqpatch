namespace EQPatch
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPatch = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.cmbPatchList = new System.Windows.Forms.ComboBox();
            this.chkAutoPlay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnPatch
            // 
            this.btnPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatch.Location = new System.Drawing.Point(176, 209);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(104, 23);
            this.btnPatch.TabIndex = 0;
            this.btnPatch.Text = "Patch";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 238);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(268, 23);
            this.progressBar.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 214);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(83, 13);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Ready to patch.";
            // 
            // cmbPatchList
            // 
            this.cmbPatchList.FormattingEnabled = true;
            this.cmbPatchList.Location = new System.Drawing.Point(176, 100);
            this.cmbPatchList.Name = "cmbPatchList";
            this.cmbPatchList.Size = new System.Drawing.Size(104, 21);
            this.cmbPatchList.TabIndex = 3;
            // 
            // chkAutoPlay
            // 
            this.chkAutoPlay.AutoSize = true;
            this.chkAutoPlay.Location = new System.Drawing.Point(153, 186);
            this.chkAutoPlay.Name = "chkAutoPlay";
            this.chkAutoPlay.Size = new System.Drawing.Size(127, 17);
            this.chkAutoPlay.TabIndex = 4;
            this.chkAutoPlay.Text = "Auto Play After Patch";
            this.chkAutoPlay.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.chkAutoPlay);
            this.Controls.Add(this.cmbPatchList);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnPatch);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EQPatch";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ComboBox cmbPatchList;
        private System.Windows.Forms.CheckBox chkAutoPlay;
    }
}

