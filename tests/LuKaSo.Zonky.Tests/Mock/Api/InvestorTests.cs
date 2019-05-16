using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    [TestClass]
    public class InvestorTests
    {
        private ZonkyApi _zonkyApi;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = ZonkyApiFactory.Create();
            _tokenProvider = new AuthorizationTokenProvider(_zonkyApi);
        }

        [TestMethod]
        public void GetWalletAsyncOk()
        {
            var wallet = _zonkyApi.GetWalletAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsNotNull(wallet.BankAccount);
            Assert.IsTrue(wallet.Balance > 0);
        }

        [TestMethod]
        public void GetWalletAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetWalletAsync(token, CancellationToken.None));
        }

        [TestMethod]
        public void GetNotificationsAsyncOk()
        {
            var notifications = _zonkyApi.GetNotificationsAsync(10, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(2, notifications.Count());
        }

        [TestMethod]
        public void GetNotificationsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetNotificationsAsync(5, token, CancellationToken.None));
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncOk()
        {
            var walletTransations = _zonkyApi.GetWalletTransactionsAsync(_tokenProvider.GetToken(), null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, walletTransations.Count());
            Assert.AreEqual("Zonky01", walletTransations.First().NickName);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetWalletTransactionsAsync(token, null, CancellationToken.None));
        }

        [TestMethod]
        public void GetBlockedAmountAsyncOk()
        {
            var blockedAmounts = _zonkyApi.GetBlockedAmountAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, blockedAmounts.Count());
            Assert.AreEqual(50, blockedAmounts.First().Amount);
        }

        [TestMethod]
        public void GetBlockedAmountAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetBlockedAmountAsync(token, CancellationToken.None));
        }
    }
}
