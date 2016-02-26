using System;

namespace FME.PasswordManager.Exceptions
{
    internal class EncryptedDeserializationException : Exception
    {
        public EncryptedDeserializationException(string message) : 
            base($"Error trying to deserialize repository, did you forget the correct master key or salt? --> Inner Exception: {message}") {}
    }
}