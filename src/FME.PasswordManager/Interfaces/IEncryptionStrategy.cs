namespace FME.PasswordManager.Interfaces
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string value);
        string Decrypt(string text);

        IConfiguration Configuration { get; set; }
    }
}
