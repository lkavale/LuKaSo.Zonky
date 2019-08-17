using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class BadResponceException : Exception
    {
        public BadResponceException(HttpResponseMessage message, Exception ex) : base($"Unexpected server responce retrieved. \r\n {message.ToString()} \r\n with content {message.Content.ReadAsStringAsync().GetAwaiter().GetResult()}", ex)
        { }

        protected BadResponceException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
