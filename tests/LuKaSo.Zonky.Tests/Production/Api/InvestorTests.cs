using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models;
using LuKaSo.Zonky.Api.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Production.Api
{
    [TestClass]
    public class InvestorTests
    {
        private ZonkyApi _zonkyClient;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyApi(new HttpClient());
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
            var size = 100;
            var notifications = _zonkyClient.GetNotificationsAsync(size, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(size, notifications.Count());
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
            var walletTransations = _zonkyClient.GetWalletTransactionsAsync(null, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransations.Count() > 1000);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncLongTermOk()
        {
            var date = DateTime.Now.AddDays(-10);
            var filter = new FilterOptions();
            filter.Add("transaction.transactionDate__gte", $"{date.Year}-{date.Month}-{date.Day}");

            var walletTransations = _zonkyClient.GetWalletTransactionsAsync(filter, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransations.Count() > 50);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetWalletTransactionsAsync(null, token, CancellationToken.None));
        }

        [TestMethod]
        public void GetBlockedAmountAsyncOk()
        {
            var blockedAmounts = _zonkyClient.GetBlockedAmountAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetBlockedAmountAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetBlockedAmountAsync(token, CancellationToken.None));
        }
    }
}
