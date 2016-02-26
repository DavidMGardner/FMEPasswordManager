using System;

namespace FMEPasswordManager.Api
{
    public class ApiParameterNullException : Exception
    {
        public ApiParameterNullException(string value) : base(value) { }
    }
}