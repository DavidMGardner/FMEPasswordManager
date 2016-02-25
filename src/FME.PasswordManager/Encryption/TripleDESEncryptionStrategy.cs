using System.Security.Cryptography;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Encryption
{
    public class TripleDESEncryptionStrategy : IEncryptionStrategy
    {
        public TripleDESEncryptionStrategy(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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

            try
            {
                return CipherUtility.Decrypt<TripleDESCryptoServiceProvider>(text, Configuration.MasterKey, Configuration.EncryptionSalt);
            }
            catch (System.Security.Cryptography.CryptographicException cryptographicException)
            {
                throw new EncryptionStrategyException(cryptographicException.Message);
            }
        }

        public IConfiguration Configuration { get; set; }
    }
}
