using LuKaSo.Zonky.Api.Models.Login;
using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Api.Exceptions
{
    public class BadRefreshTokenException : Exception
    {
        public BadRefreshTokenException(HttpResponseMessage message, AuthorizationToken token) : base($"Bad refresh token {token.RefreshToken}. \r\n Server return \r\n {message.ToString()}")
        { }
    }
}
