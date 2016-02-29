﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FMEPasswordManager.Api
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Bad Request", context.Exception);
            var httpError = (HttpError)((ObjectContent<HttpError>)context.Response.Content).Value;

            if (!httpError.ContainsKey("ExceptionType"))
                httpError.Add("ExceptionType", context.Exception.GetType().FullName);
            if (!httpError.ContainsKey("ExceptionMessage"))
                httpError.Add("ExceptionMessage", context.Exception.Message);
            if (httpError.ContainsKey("StackTrace"))
                httpError.Remove("StackTrace");
        }
    }
}