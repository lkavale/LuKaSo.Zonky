using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Api.Tests
{
    [TestClass]
    public class LoginTests
    {
        private ZonkyApi _zonkyClient;
        private Secrets _secrets;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyApi(new HttpClient());
            _secrets = new SecretsJsonReader().Read();
        }

        [TestMethod]
        public void LoginOk()
        {
            
            var token = _zonkyClient.GetTokenExchangePasswordAsync(_secrets.LoginOk, CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void LoginBad()
        {
            Assert.ThrowsExceptionAsync<BadLoginException>(() => _zonkyClient.GetTokenExchangePasswordAsync(_secrets.LoginWrong, CancellationToken.None));
        }

        [TestMethod]
        public void RefreshTokenOk()
        {
            var tokenLogin = _zonkyClient.GetTokenExchangePasswordAsync(_secrets.LoginOk, CancellationToken.None).GetAwaiter().GetResult();
            var tokenRefreshed = _zonkyClient.GetTokenExchangeRefreshTokenAsync(tokenLogin, CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void RefreshTokenBad()
        {
            AuthorizationToken token = new AuthorizationToken() { RefreshToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<BadRefreshTokenException>(() => _zonkyClient.GetTokenExchangeRefreshTokenAsync(token, CancellationToken.None));
        }
    }
}
