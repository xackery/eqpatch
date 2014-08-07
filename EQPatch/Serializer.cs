using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonFx.Json;
using System.Security.Cryptography;
using System.IO;

namespace EQPatch
{
    class Serializer
    {  
            private static MD5 md5Hash;
            public static string SerializeToString(object objectInstance)
            {
                return JsonWriter.Serialize(objectInstance);
            }

            public static T DeserializeFromString<T>(string stringData)
            {
                if (stringData.Length < 1) return default(T);
                return JsonReader.Deserialize<T>(stringData);
            }

            public static string GetMD5HashFromFile(string filePath)
            {
                if (md5Hash == null) md5Hash = MD5.Create();
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5Hash.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }

            public static bool CompareFileToMD5(string filePath, string hash)
            {
                string firstHash = GetMD5HashFromFile(filePath);
                Console.WriteLine(firstHash + " vs " + hash);
                return (firstHash == hash);
            }
    }
}
