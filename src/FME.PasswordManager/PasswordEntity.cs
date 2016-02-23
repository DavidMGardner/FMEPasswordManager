namespace FME.PasswordManager
{
    public interface IEntity
    {
        string Id { get; set; }
    }

    public class PasswordEntity : IEntity
    {
        public string Id { get; set; }
        public string CommonName { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}