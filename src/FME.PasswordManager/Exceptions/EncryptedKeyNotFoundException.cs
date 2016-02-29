using System;

namespace FME.PasswordManager.Exceptions
{
    public class EncryptedKeyNotFoundException : Exception
    {
        public EncryptedKeyNotFoundException(string encryptedkeyNotFoundInKeyStoreHasYourSessionExpired) : base(encryptedkeyNotFoundInKeyStoreHasYourSessionExpired) { }
    }
}