using FME.PasswordManager;

namespace FMEPasswordManager.Api.Models
{
    public class PasswordManagementDomainModel
    {
        public PasswordEntity Entity { get; set; }
        public string MasterKey { get; set; }
    }
}