using LuKaSo.Zonky.Models.Login;
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class BadRefreshTokenException : Exception
    {
        public BadRefreshTokenException(HttpResponseMessage message, AuthorizationToken token) : base($"Bad refresh token {token.RefreshToken.ToString()}. \r\n Server return \r\n {message.ToString()}")
        { }

        protected BadRefreshTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
