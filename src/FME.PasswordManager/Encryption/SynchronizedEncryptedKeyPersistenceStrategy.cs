using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using FME.PasswordManager.Configuration;
using FME.PasswordManager.Exceptions;
using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager.Encryption
{
    public class SynchronizedEncryptedKeyPersistenceStrategy : IKeyPersistenceStrategy
    {
        private readonly Lazy<IConfiguration> _configuration;
        private readonly ConcurrentDictionary<string, KeyState> _concurrentState = new ConcurrentDictionary<string, KeyState>();

        public SynchronizedEncryptedKeyPersistenceStrategy(Lazy<IConfiguration> configuration)
        {
            _configuration = configuration;
        }


        public string AddOrRetrieveMasterKey(string masterKey)
        {
            var encryptedKey = CipherUtility.Encrypt<TripleDESCryptoServiceProvider>(masterKey, masterKey, _configuration.Value.EncryptionSalt);

            KeyState outKeyState;
            if (_concurrentState.TryGetValue(encryptedKey, out outKeyState))
            {
                return outKeyState.EnryptedMasterKey;
            }

            var newKeyState = new KeyState()
            {
                DecryptedMasterKey = masterKey,
                EnryptedMasterKey = encryptedKey
            };

            if (_concurrentState.TryAdd(encryptedKey, newKeyState))
            {
                return newKeyState.EnryptedMasterKey;
            }

            return String.Empty;
        }

        public string DecryptedMasterKey(string encryptedMaster)
        {
            KeyState outKeyState;
            if (_concurrentState.TryGetValue(encryptedMaster, out outKeyState))
            {
                return outKeyState.DecryptedMasterKey;
            }

            return null;
        }

        public string EncryptedMasterKey(string encryptedMaster)
        {
            KeyState outKeyState;
            if (_concurrentState.TryGetValue(encryptedMaster, out outKeyState))
            {
                return outKeyState.EnryptedMasterKey;
            }

            throw new EncryptedKeyNotFoundException("EncryptedKey Not Found in Key Store, has your session expired?");
        }

        internal class KeyState
        {
            public string DecryptedMasterKey { get; set; }
            public string EnryptedMasterKey { get; set; }
        }
    }
}