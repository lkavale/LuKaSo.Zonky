using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Api.Exceptions
{
    public class ServerErrorException : Exception
    {
        public ServerErrorException(HttpResponseMessage message) : base($"Unexpected server responce retrieved. \r\n {message.ToString()}")
        { }
    }
}
