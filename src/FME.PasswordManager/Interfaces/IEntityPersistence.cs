using System.Collections.Generic;

namespace FME.PasswordManager.Interfaces
{
    public interface IEntityPersistence<T> 
    {
        bool PutList(List<T> entities);
        bool AddRange(List<T> entities);
        List<T> GetList();
    }
}