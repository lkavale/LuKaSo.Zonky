using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
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
        private ZonkyApi _zonkyApi;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
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
            var size = 100;
            var notifications = _zonkyApi.GetNotificationsAsync(0, size, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(size, notifications.Count());
        }

        [TestMethod]
        public void GetNotificationsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetNotificationsAsync(0, 5, token, CancellationToken.None));
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncOk()
        {
            var walletTransations = _zonkyApi.GetWalletTransactionsAsync(_tokenProvider.GetToken(), null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransations.Count() > 1000);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncLongTermOk()
        {
            var date = DateTime.Now.AddDays(-10);
            var filter = new FilterOptions();
            filter.Add("transaction.transactionDate__gte", $"{date.Year}-{date.Month}-{date.Day}");

            var walletTransations = _zonkyApi.GetWalletTransactionsAsync(_tokenProvider.GetToken(), filter, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransations.Count() > 50);
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

            Assert.IsNotNull(blockedAmounts);
        }

        [TestMethod]
        public void GetBlockedAmountAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetBlockedAmountAsync(token, CancellationToken.None));
        }
    }
}
