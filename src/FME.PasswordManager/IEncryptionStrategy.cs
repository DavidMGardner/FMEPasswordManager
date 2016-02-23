using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FME.PasswordManager
{
    public interface IEncryptionStrategy
    {
        string Encrypt(string value);
        string Decrypt(string text);

        IConfiguration Configuration { get; set; }
    }
}
