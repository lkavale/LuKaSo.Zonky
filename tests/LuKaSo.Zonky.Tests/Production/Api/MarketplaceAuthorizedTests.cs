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
    public class MarketplaceAuthorizedTests
    {
        private ZonkyApi _zonkyApi;
        private AuthorizationToken _token;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
            _token = new AuthorizationTokenProvider(_zonkyApi).GetToken();
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, _token, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loans.Count());
        }

        [TestMethod]
        public void GetPrimaryMarketplaceNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetPrimaryMarketPlaceAsync(0, 10, token, null, CancellationToken.None));
        }

        [TestMethod]
        public void GetPrimaryMarketplaceInvestableOk()
        {
            var pageSize = 500;
            var filter = new FilterOptions();
            filter.Add("nonReservedRemainingInvestment__gt", "0");

            var loansAll = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, _token, null, CancellationToken.None).GetAwaiter().GetResult();
            var loans = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, _token, filter, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loansAll.Count());
            Assert.AreNotEqual(pageSize, loans.Count());
            Assert.AreNotEqual(loansAll.Count(l => !l.Covered), loans.Count());
            Assert.AreEqual(0, loans.Count(l => l.Covered));
        }

        [TestMethod]
        public void GetSecondaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyApi.GetSecondaryMarketplaceAsync(0, pageSize, _token, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsNotNull(loans);
        }

        [TestMethod]
        public void GetSecondaryMarketplaceNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetSecondaryMarketplaceAsync(0, 10, token, null, CancellationToken.None));
        }
    }
}
