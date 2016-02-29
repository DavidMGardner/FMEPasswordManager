using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FME.PasswordManager.Configuration;

namespace FMEPasswordManager.Api
{
    public class MasterKeyHeaderFilter : ActionFilterAttribute
    {
        public IConfiguration Configuration { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var encryptedMasterKey = actionContext.Request.GetFirstHeaderValueOrDefault<string>("X-Key");
            var masterKey = actionContext.Request.GetFirstHeaderValueOrDefault<string>("X-MasterKey");

            if (String.IsNullOrWhiteSpace(encryptedMasterKey) && String.IsNullOrWhiteSpace(masterKey))
                throw new ApiParameterNullException("MasterKey was not been provided via http header");

            if (!String.IsNullOrWhiteSpace(masterKey))
                Configuration.MasterKey = masterKey;

            if (!String.IsNullOrWhiteSpace(encryptedMasterKey))
                Configuration.EncryptedMasterKey = encryptedMasterKey;

            base.OnActionExecuting(actionContext);
        }
    }
}