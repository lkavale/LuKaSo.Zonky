using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Api.Exceptions
{
    public class BadResponceException : Exception
    {
        public BadResponceException(HttpResponseMessage message, Exception ex) : base($"Unexpected server responce retrieved. \r\n {message.ToString()}", ex)
        { }
    }
}
