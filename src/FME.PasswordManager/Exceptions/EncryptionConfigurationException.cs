using System;

namespace FME.PasswordManager.Exceptions
{
    public class EncryptionConfigurationException : Exception
    {
        public EncryptionConfigurationException(string message) : base(message) {}
    }
}