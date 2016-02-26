using System.Security.Cryptography;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Encryption
{
    public class TripleDESEncryptionStrategy : IEncryptionStrategy
    {
        private readonly IConfiguration _configuration;

        public TripleDESEncryptionStrategy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Encrypt(string value)
        {
            if(_configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            return CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(value, _configuration.MasterKey, _configuration.EncryptionSalt);
        }

        public string Decrypt(string text)
        {
            if (_configuration == null)
                throw new EncryptionConfigurationException("Invalid configuration set in TripleDESEncryptionStrategy");

            try
            {
                return CipherUtility.Decrypt<TripleDESCryptoServiceProvider>(text, _configuration.MasterKey, _configuration.EncryptionSalt);
            }
            catch (System.Security.Cryptography.CryptographicException cryptographicException)
            {
                throw new EncryptionStrategyException(cryptographicException.Message);
            }
        }
    }
}
