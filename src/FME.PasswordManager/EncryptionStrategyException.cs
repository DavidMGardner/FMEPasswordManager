using System;

namespace FME.PasswordManager
{
    public class EncryptionStrategyException : Exception
    {
        public EncryptionStrategyException(string message) : base($"Unable to Decrypt most likely due to mismatched salt or Master Key --> Inner Crypto Exception Message: {message}") { }
    }
}