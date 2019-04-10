using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Api.Tests
{
    [TestClass]
    public class MarketPlaceTests
    {
        private ZonkyApi _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyApi(new HttpClient());
        }

        [TestMethod]
        public void GetPrimaryMarketPlaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetPrimaryMarketPlaceAsync(0, pageSize, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, loans.Count());
        }

        [TestMethod]
        public void GetSecondaryMarketPlaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetSecondaryMarketplaceAsync(0, pageSize, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
