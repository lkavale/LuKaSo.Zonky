using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class LinkNotFoundException : Exception
    {
        public LinkNotFoundException(HttpResponseMessage message, Exception ex) : base($"Desired link could not be found. \r\nServer return \r\n {message.ToString()} \r\n with content {message.Content.ReadAsStringAsync().GetAwaiter().GetResult()}", ex)
        { }

        protected LinkNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
