using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Tests.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Api.Tests
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
            var notifications = _zonkyClient.GetNotificationsAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetNotificationsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetNotificationsAsync(token, CancellationToken.None));
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncOk()
        {
            var walletTransations = _zonkyClient.GetWalletTransactionsAsync(_tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransations.Count() > 0);
        }

        [TestMethod]
        public void GetWalletTransactionsAsyncNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetWalletTransactionsAsync(token, CancellationToken.None));
        }
    }
}
