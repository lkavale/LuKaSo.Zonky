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
        private ZonkyApi _zonkyClient;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = ZonkyApiFactory.Create();
            _tokenProvider = new AuthorizationTokenProvider(_zonkyClient);
        }

        [TestMethod]
        public void GetWalletAsyncOk()
        {
            var wallet = _zonkyClient.GetWalletAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsNotNull(wallet.BankAccount);
            Assert.IsTrue(wallet.Balance > 0);
        }

        [TestMethod]
        public void GetWalletAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetWalletAsync(token, CancellationToken.None));
        }

        [TestMethod]
        public void GetNotificationsAsyncOk()
        {
            var notifications = _zonkyClient.GetNotificationsAsync(10, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(2, notifications.Count());
        }

        [TestMethod]
        public void GetNotificationsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetNotificationsAsync(5, token, CancellationToken.None));
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncOk()
        {
            var walletTransations = _zonkyClient.GetWalletTransactionsAsync(_tokenProvider.GetToken(), null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, walletTransations.Count());
            Assert.AreEqual("Zonky01", walletTransations.First().NickName);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetWalletTransactionsAsync(token, null, CancellationToken.None));
        }

        [TestMethod]
        public void GetBlockedAmountAsyncOk()
        {
            var blockedAmounts = _zonkyClient.GetBlockedAmountAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, blockedAmounts.Count());
            Assert.AreEqual(50, blockedAmounts.First().Amount);
        }

        [TestMethod]
        public void GetBlockedAmountAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetBlockedAmountAsync(token, CancellationToken.None));
        }
    }
}
