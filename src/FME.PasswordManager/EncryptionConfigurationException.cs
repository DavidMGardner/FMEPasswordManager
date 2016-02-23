using System;

namespace FME.PasswordManager
{
    public class EncryptionConfigurationException : Exception
    {
        public EncryptionConfigurationException(string message) : base(message) {}
    }
}