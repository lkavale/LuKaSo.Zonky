using LuKaSo.Zonky.Models.Login;
using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Exceptions
{
    public class BadRefreshTokenException : Exception
    {
        public BadRefreshTokenException(HttpResponseMessage message, AuthorizationToken token) : base($"Bad refresh token {token.RefreshToken.ToString()}. \r\n Server return \r\n {message.ToString()}")
        { }
    }
}
