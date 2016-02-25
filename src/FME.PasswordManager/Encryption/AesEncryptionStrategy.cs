using System.Security.Cryptography;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Encryption
{
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        public AesEncryptionStrategy(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Encrypt(string value)
        {
            if (Configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            return CipherUtility.Encrypt<AesCryptoServiceProvider>(value, Configuration.MasterKey, Configuration.EncryptionSalt);
        }

        public string Decrypt(string text)
        {
            if (Configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            return CipherUtility.Decrypt<AesCryptoServiceProvider>(text, Configuration.MasterKey, Configuration.EncryptionSalt);
        }

        public IConfiguration Configuration { get; set; }
    }
}