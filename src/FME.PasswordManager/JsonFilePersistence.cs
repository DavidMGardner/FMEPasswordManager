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
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public JsonFilePersistence(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private string GetFullPath()
        {
            if (string.IsNullOrWhiteSpace(_configuration.StorageLocation) || string.IsNullOrWhiteSpace(_configuration.FileName))
                throw new Exception("Path appsettings are missing");                

            return $"{_configuration.StorageLocation}\\{_configuration.FileName}";
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
                using (StreamReader file = File.OpenText(GetFullPath()))
                {
                    return JsonConvert.DeserializeObject<List<T>>(file.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        } 

        private bool SerializeObject(object o)
        {
            try
            {
                EnsureFolder(_configuration.StorageLocation);
                using (FileStream fs = File.Open(GetFullPath(), FileMode.OpenOrCreate))
                { 
                    string json = JsonConvert.SerializeObject(o);
                    string encrypted = EncryptionStrategy.Encrypt(json);

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

        public IEncryptionStrategy EncryptionStrategy { get; set; }
    }
}