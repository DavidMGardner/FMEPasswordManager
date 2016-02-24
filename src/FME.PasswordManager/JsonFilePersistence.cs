using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Serilog;

namespace FME.PasswordManager
{
    public class JsonFilePersistence<T> : IEntityPersistence<T>
    {
        private IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IEncryptionStrategy _encryptionStrategy;

        public string MasterKey
        {
            set { _encryptionStrategy.Configuration.MasterKey = value; }
        }

        public IConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        public JsonFilePersistence(IConfiguration configuration, ILogger logger, IEncryptionStrategy encryptionStrategy)
        {
            _logger = logger;
            _encryptionStrategy = encryptionStrategy;
            _configuration = configuration;
        }

     

        private static void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (!String.IsNullOrEmpty(directoryName) && (!Directory.Exists(directoryName)))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        private List<T> DeserializeObject()
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
                    return JsonConvert.DeserializeObject<List<T>>(_encryptionStrategy.Decrypt(encryptedFile));
                }
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                _logger.Error(ex.Message);
                throw new EncryptedDeserializationException(ex.Message);
            }
        } 

        private bool SerializeObject(object o)
        {
            try
            {
                EnsureFolder(_configuration.StorageLocation);
                using (FileStream fs = File.Open(_configuration.GetFullPath(), FileMode.OpenOrCreate))
                { 
                    string json = JsonConvert.SerializeObject(o);
                    string encrypted = _encryptionStrategy.Encrypt(json);

                    byte[] info = new UTF8Encoding(true).GetBytes(encrypted);
                    fs.Write(info, 0, info.Length);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return false;
        }

        public bool PutList(List<T> entities)
        {
            return SerializeObject(entities);
        }

        public bool AddRange(List<T> entities)
        {
            var list = GetList();
            list.AddRange(entities);
            return PutList(list);
        }

        public List<T> GetList()
        {
            return DeserializeObject();
        }
    }

    internal class EncryptedDeserializationException : Exception
    {
        public EncryptedDeserializationException(string message) : 
            base($"Error trying to deserialize repository, did you forget the correct master key or salt? --> Inner Exception: {message}") {}
    }
}