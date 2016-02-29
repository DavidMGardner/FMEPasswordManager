using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using FME.PasswordManager;
using FME.PasswordManager.Interfaces;

namespace FMEPasswordManager.Api
{
    public static class ApiControllerExtensions
    {
        public static void SetMasterKey(this ApiController controller, IKey masterKey )
        {
            var encryptedMasterKey = controller.Request.GetFirstHeaderValueOrDefault<string>("X-MasterKey");
            if (String.IsNullOrWhiteSpace(encryptedMasterKey))
                throw new ApiParameterNullException("MasterKey was not been provided via http header");
            
            masterKey.MasterKey = encryptedMasterKey;
        }
    }
}