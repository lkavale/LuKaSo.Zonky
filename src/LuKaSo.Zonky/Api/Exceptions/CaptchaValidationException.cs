using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Api.Exceptions
{
    public class CaptchaValidationException : Exception
    {
        public CaptchaValidationException(HttpResponseMessage reponce) : base($"Captch validation failed. \r\nServer return \r\n{reponce.ToString()}")
        { }
    }
}
