namespace FME.PasswordManager.Interfaces
{
    public interface IKeyPersistenceStrategy
    {
        string Key { set; }
        void SetEncryptKey(string encryptedKey);
        string EncryptedKey { get; }
        string DecryptedKey { get; }
    }
}