namespace FME.PasswordManager.Interfaces
{
    public interface IKeyPersistenceStrategy
    {
        string AddOrRetrieveMasterKey(string masterKey);
        string DecryptedMasterKey(string encryptedMaster);
        string EncryptedMasterKey(string encryptedMaster);
    }
}