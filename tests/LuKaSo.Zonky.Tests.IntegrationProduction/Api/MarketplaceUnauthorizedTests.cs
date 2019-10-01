using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Api
{
    [TestClass]
    public class MarketplaceUnauthorizedTests
    {
        private ZonkyApi _zonkyApi;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loans.Count());
        }

        [TestMethod]
        public void GetPrimaryMarketplaceInvestableOk()
        {
            var pageSize = 500;
            var filter = new FilterOptions();
            filter.Add("nonReservedRemainingInvestment__gt", "0");

            var loansAll = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();
            var loans = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, filter, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loansAll.Count());
            Assert.AreNotEqual(pageSize, loans.Count());
            Assert.AreNotEqual(loansAll.Count(l => !l.Covered), loans.Count());
            Assert.AreEqual(0, loans.Count(l => l.Covered));
        }

        [TestMethod]
        public void GetSecondaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyApi.GetSecondaryMarketplaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsNotNull(loans);
        }
    }
}

