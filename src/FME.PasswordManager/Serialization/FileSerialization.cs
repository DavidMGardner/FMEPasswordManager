using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;
using FME.PasswordManager.Persistence;
using Newtonsoft.Json;
using Serilog;

namespace FME.PasswordManager.Serialization
{
    public class FileSerialization<T> : ISerialization<T>
    {
        private readonly IConfiguration _configuration;
        private readonly IEncryptionStrategy _encryptionStrategy;
        private readonly ILogger _logger;

        public FileSerialization(IConfiguration configuration, IEncryptionStrategy encryptionStrategy, ILogger logger)
        {
            _configuration = configuration;
            _encryptionStrategy = encryptionStrategy;
            _logger = logger;
        }

        private static void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!String.IsNullOrEmpty(directoryName) && (!Directory.Exists(directoryName)))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public bool SerializeObject(object o)
        {
            try
            {
                EnsureFolder(_configuration.StorageLocation);

                string json = JsonConvert.SerializeObject(o);
                string encrypted = _encryptionStrategy.Encrypt(json);

                File.WriteAllText(_configuration.GetFullPath(), encrypted);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return false;
        }

        public List<T> DeserializeObject()
        {
            try
            {
                EnsureFolder(_configuration.StorageLocation);

                if (File.Exists(_configuration.GetFullPath()) == false)
                {
                    return new List<T>();
                }

                using (StreamReader file = File.OpenText(_configuration.GetFullPath()))
                {
                    string encryptedFile = file.ReadToEnd();

                    string decriptedText = _encryptionStrategy.Decrypt(encryptedFile);

                    return JsonConvert.DeserializeObject<List<T>>(decriptedText);
                }
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                _logger.Error(ex.Message);
                throw new EncryptedDeserializationException(ex.Message);
            }
        }

    }
}
