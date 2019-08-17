using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class CaptchaValidationException : Exception
    {
        public CaptchaValidationException(HttpResponseMessage message) : base($"Captch validation failed. \r\nServer return \r\n {message.ToString()} \r\n with content {message.Content.ReadAsStringAsync().GetAwaiter().GetResult()}")
        { }

        protected CaptchaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
