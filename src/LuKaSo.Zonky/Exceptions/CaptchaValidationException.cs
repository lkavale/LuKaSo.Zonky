using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class CaptchaValidationException : Exception
    {
        public CaptchaValidationException(HttpResponseMessage reponce) : base($"Captch validation failed. \r\nServer return \r\n{reponce.ToString()}")
        { }

        protected CaptchaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
