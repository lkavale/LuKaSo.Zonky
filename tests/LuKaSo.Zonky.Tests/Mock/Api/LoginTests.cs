using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    [TestClass]
    public class LoginTests
    {
        private ZonkyApi _zonkyApi;
        private User _zonkyLoginOk;
        private User _zonkyLoginWrong;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = ZonkyApiFactory.Create();
            _zonkyLoginOk = new User("test", "test");
            _zonkyLoginWrong = new User("tset", "tset");
        }

        [TestMethod]
        public void LoginOk()
        {
            var token = _zonkyApi.GetTokenExchangePasswordAsync(_zonkyLoginOk, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(new Guid("c5f6b996-47aa-4c59-8fc7-8a03fcf5da9d"), token.AccessToken);
            Assert.AreEqual(AuthorizationTokenType.bearer, token.TokenType);
            Assert.AreEqual(new Guid("d33c18a7-cc94-4e35-9ac3-c67528a602f4"), token.RefreshToken);
            Assert.AreEqual(299, token.ExpiresIn);
            Assert.AreEqual(AuthorizationScope.SCOPE_APP_WEB, token.Scope);
        }

        [TestMethod]
        public void LoginBad()
        {
            Assert.ThrowsExceptionAsync<BadLoginException>(() => _zonkyApi.GetTokenExchangePasswordAsync(_zonkyLoginWrong, CancellationToken.None));
        }

        [TestMethod]
        public void RefreshTokenOk()
        {
            var tokenLogin = _zonkyApi.GetTokenExchangePasswordAsync(_zonkyLoginOk, CancellationToken.None).GetAwaiter().GetResult();
            var tokenRefreshed = _zonkyApi.GetTokenExchangeRefreshTokenAsync(tokenLogin, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(new Guid("c5f6b996-47aa-4c59-8fc7-8a03fcf5da9d"), tokenRefreshed.AccessToken);
            Assert.AreEqual(AuthorizationTokenType.bearer, tokenRefreshed.TokenType);
            Assert.AreEqual(new Guid("d33c18a7-cc94-4e35-9ac3-c67528a602f4"), tokenRefreshed.RefreshToken);
            Assert.AreEqual(299, tokenRefreshed.ExpiresIn);
            Assert.AreEqual(AuthorizationScope.SCOPE_APP_WEB, tokenRefreshed.Scope);
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
