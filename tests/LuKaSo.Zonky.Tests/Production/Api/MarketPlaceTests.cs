using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Production.Api
{
    [TestClass]
    public class MarketplaceTests
    {
        private ZonkyApi _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyApi(new HttpClient());
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loans.Count());
        }

        [TestMethod]
        public void GetPrimaryMarketplaceInvestableOk()
        {
            var pageSize = 500;
            var filter = new FilterOptions();
            filter.Add("nonReservedRemainingInvestment__gt", "0");

            var loansAll = _zonkyClient.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();
            var loans = _zonkyClient.GetPrimaryMarketPlaceAsync(0, pageSize, filter, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loansAll.Count());
            Assert.AreNotEqual(pageSize, loans.Count());
            Assert.AreNotEqual(loansAll.Where(l => !l.Covered).Count(), loans.Count());
            Assert.AreEqual(0, loans.Where(l => l.Covered).Count());
        }

        [TestMethod]
        public void GetSecondaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetSecondaryMarketplaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
