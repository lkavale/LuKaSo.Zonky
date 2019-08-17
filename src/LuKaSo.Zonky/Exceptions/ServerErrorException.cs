using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class ServerErrorException : Exception
    {
        public ServerErrorException(HttpResponseMessage message) : base($"Unexpected server responce retrieved. \r\n {message.ToString()} \r\n with content {message.Content.ReadAsStringAsync().GetAwaiter().GetResult()}")
        { }

        protected ServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
