using System.Collections.Generic;

namespace FME.PasswordManager
{
    public interface IEntityPersistence<T> 
    {
        bool PutList(List<T> entities);
        bool AddRange(List<T> entities);
        List<T> GetList();

        IConfiguration Configuration { get; set; }
    }
}