using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FME.PasswordManager
{
    public class Configuration : IConfiguration
    {
        public string StorageLocation => ConfigurationManager.AppSettings["StorageLocation"];
        public string FileName => ConfigurationManager.AppSettings["FileName"];
        public string EncryptionSalt
        {
            get { return ConfigurationManager.AppSettings["EncryptionSalt"]; }
            set { ConfigurationManager.AppSettings["EncryptionSalt"] = value; }
        }
        public string MasterKey { get; set; }
        
    }

    public interface IConfiguration
    {
        string StorageLocation { get; }
        string FileName { get; }
        string MasterKey { get; set; }
        string EncryptionSalt { get; set; }
    }
}
