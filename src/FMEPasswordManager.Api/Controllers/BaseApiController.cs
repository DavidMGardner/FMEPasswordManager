using System;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Configuration;

namespace FMEPasswordManager.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        //protected IConfiguration FMEConfiguration { get; set; }
        //protected IRepository<PasswordEntity> Repository { get; set; }

        //public void SetEncryptedMasterKey()
        //{
        //    var encryptedMasterKey = Request.GetFirstHeaderValueOrDefault<string>("X-Key");
        //    var masterKey = Request.GetFirstHeaderValueOrDefault<string>("X-MasterKey");

        //    if (String.IsNullOrWhiteSpace(encryptedMasterKey) && String.IsNullOrWhiteSpace(masterKey))
        //        throw new ApiParameterNullException("MasterKey was not been provided via http header");

        //    if (!String.IsNullOrWhiteSpace(masterKey))
        //        FMEConfiguration.MasterKey = masterKey;

        //    if (!String.IsNullOrWhiteSpace(encryptedMasterKey))
        //        FMEConfiguration.EncryptedMasterKey = encryptedMasterKey;
        //}
    }
}