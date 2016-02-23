﻿namespace FME.PasswordManager
{
    public class NullEncryptionStrategy : IEncryptionStrategy
    {
        public string Encrypt(string value)
        {
            return value;
        }

        public string Decrypt(string text)
        {
            return text;
        }

        public IConfiguration Configuration { get; set; }
    }
}