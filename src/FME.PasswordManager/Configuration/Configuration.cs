using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Configuration
{
    public class Configuration : IConfiguration
    {
        public string StorageLocation => ConfigurationManager.AppSettings["StorageLocation"];
        public string EncryptionSalt
        {
            get { return ConfigurationManager.AppSettings["EncryptionSalt"]; }
            set { ConfigurationManager.AppSettings["EncryptionSalt"] = value; }
        }

        public string FileExtension => EnvironmentName;
        public string EnvironmentName => String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Environment"]) ? "Production" : ConfigurationManager.AppSettings["Environment"];
        
        public string EncriptedFileName 
        {
            get
            {
                var encryptedFilename = EncryptedMasterKey;
                var encodedFileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedFilename));
                var encodedAndEscapedFilename = encodedFileName.Replace('/', '-');

                return $"{encodedAndEscapedFilename}.{FileExtension}";
            }
        }

        public string DecryptedMasterKey { get; private set; }
        public string EncryptedMasterKey { get; private set; }
        
        // shared resource
        public string MasterKey
        {
            set
            {
                DecryptedMasterKey = value;
                EncryptedMasterKey = CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(DecryptedMasterKey, DecryptedMasterKey, EncryptionSalt);
            }
        }

        public string GetFullPath()
        {
            if (string.IsNullOrWhiteSpace(StorageLocation) || string.IsNullOrWhiteSpace(EncriptedFileName))
                throw new Exception("Path appsettings are missing");

            return $"{StorageLocation}\\{EncriptedFileName}";
        }

    }

    public interface IConfiguration
    {
        string StorageLocation { get; }
        string EncriptedFileName { get; }
        string EncryptionSalt { get; set; }
        string GetFullPath();

        string MasterKey { set; }
        string DecryptedMasterKey { get; }
        string EncryptedMasterKey { get; }
    }
}
