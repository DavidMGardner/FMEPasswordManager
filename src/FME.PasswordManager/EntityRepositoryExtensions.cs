using FME.PasswordManager.Interfaces;

namespace FME.PasswordManager
{
    public static class EntityRepositoryExtensions
    {
        public static void MasterKey(this IRepository<PasswordEntity> entityRepository, string masterKey)
        {
            IKey key = entityRepository as IKey;
            if (key != null) key.MasterKey = masterKey;
        }
    }
}