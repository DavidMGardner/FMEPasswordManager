using System;
using System.Security.Cryptography;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Encryption
{
    public class SynchronizedEncryptedKeyPersistenceStrategy : IKeyPersistenceStrategy
    {
        private readonly string _encryptionSalt;
        private string _decryptedKey = String.Empty;
        private string _encryptedKey = String.Empty;
        private readonly object _lock = new object();

        public SynchronizedEncryptedKeyPersistenceStrategy(string encryptionSalt)
        {
            _encryptionSalt = encryptionSalt;
        }

        public string EncryptedKey => String.IsNullOrWhiteSpace(_encryptedKey) ? null : _encryptedKey;
        public string DecryptedKey => String.IsNullOrWhiteSpace(_encryptedKey) ? null : _decryptedKey;

        public void SetEncryptKey(string encryptedKey)
        {
            _encryptedKey = encryptedKey;
        }

        public string Key
        {
            set
            {
                lock (_lock)
                {
                    _decryptedKey = value;
                    _encryptedKey = CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(_decryptedKey, value, _encryptionSalt);
                }
            }
        }

    }
}