using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FME.PasswordManager
{
    public class TripleDESEncryptionStrategy : IEncryptionStrategy
    {
       public string Encrypt(string value)
        {
            if(Configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            return CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(value, Configuration.MasterKey, Configuration.EncryptionSalt);
        }

        public string Decrypt(string text)
        {
            if (Configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            return CipherUtility.Decrypt<TripleDESCryptoServiceProvider>(text, Configuration.MasterKey, Configuration.EncryptionSalt);
        }

        public IConfiguration Configuration { get; set; }
    }
}
