using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Production.Api
{
    [TestClass]
    public class LoginTests
    {
        private ZonkyApi _zonkyApi;
        private Secrets _secrets;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
            _secrets = new SecretsJsonReader().Read();
        }

        [TestMethod]
        public void LoginOk()
        {
            var token = _zonkyApi.GetTokenExchangePasswordAsync(_secrets.LoginOk, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void LoginBad()
        {
            Assert.ThrowsExceptionAsync<BadLoginException>(() => _zonkyApi.GetTokenExchangePasswordAsync(_secrets.LoginWrong, CancellationToken.None));
        }

        [TestMethod]
        public void RefreshTokenOk()
        {
            var tokenLogin = _zonkyApi.GetTokenExchangePasswordAsync(_secrets.LoginOk, CancellationToken.None).GetAwaiter().GetResult();
            var tokenRefreshed = _zonkyApi.GetTokenExchangeRefreshTokenAsync(tokenLogin, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreNotEqual(tokenLogin.AccessToken, tokenRefreshed.AccessToken);
            Assert.AreEqual(tokenLogin.RefreshToken, tokenRefreshed.RefreshToken); //If refresh token is not expired, tokens are equal 
            Assert.AreEqual(tokenLogin.Scope, tokenRefreshed.Scope);
            Assert.AreEqual(tokenLogin.TokenType, tokenRefreshed.TokenType);
        }

        [TestMethod]
        public void RefreshTokenBad()
        {
            AuthorizationToken token = new AuthorizationToken() { RefreshToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<BadRefreshTokenException>(() => _zonkyApi.GetTokenExchangeRefreshTokenAsync(token, CancellationToken.None));
        }

        [TestMethod]
        public void RefreshTokenMissing()
        {
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetTokenExchangeRefreshTokenAsync(null, CancellationToken.None));
        }
    }
}
