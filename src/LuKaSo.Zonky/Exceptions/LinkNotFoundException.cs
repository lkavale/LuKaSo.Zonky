using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class LinkNotFoundException : Exception
    {
        public LinkNotFoundException(HttpResponseMessage reponce, Exception ex) : base($"Desired link could not be found. \r\nServer return \r\n{reponce.ToString()}", ex)
        { }

        protected LinkNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
