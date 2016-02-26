using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using FME.PasswordManager.Encryption;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Configuration
{
    public class SynchronizedConfiguration : IConfiguration
    {
        private readonly object _lock = new object();
        private string _masterKey = String.Empty;

        public string StorageLocation => ConfigurationManager.AppSettings["StorageLocation"];
        public string EncryptionSalt
        {
            get { return ConfigurationManager.AppSettings["EncryptionSalt"]; }
            set { ConfigurationManager.AppSettings["EncryptionSalt"] = value; }
        }

        public string EncriptedFileName 
        {
            get
            {
                var encryptedFilename = CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(MasterKey, MasterKey, EncryptionSalt);
                var encodedFileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedFilename));
                var encodedAndEscapedFilename = encodedFileName.Replace('/', '-');

                return encodedAndEscapedFilename;
            }
        }

        // shared resource
        public string MasterKey
        {
            get { return _masterKey; }
            set
            {
                lock (_lock)
                {
                    _masterKey = value;
                }
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
        string MasterKey { get; set; }
        string EncryptionSalt { get; set; }
        string GetFullPath();
    }
}
