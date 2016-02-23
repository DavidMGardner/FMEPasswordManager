using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FME.PasswordManager
{
    public interface IPasswordManagement
    {
        string GeneratePassword();
    }

    public class PasswordManagement : IPasswordManagement
    {
        public string GeneratePassword()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
