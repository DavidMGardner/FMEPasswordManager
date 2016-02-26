using System.Collections.Generic;

namespace FME.PasswordManager.Interfaces
{
    public interface ISerialization<T>
    {
        List<T> DeserializeObject();
        bool SerializeObject(object o);
    }
}