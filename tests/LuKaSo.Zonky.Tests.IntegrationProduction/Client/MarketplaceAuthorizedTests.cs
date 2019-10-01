using LuKaSo.Zonky.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Client
{
    [TestClass]
    public class MarketplaceAuthorizedTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyClient();
        }

        [TestMethod]
        [TestCategory("Full")]
        public void GetEntirePrimaryMarketplaceOk()
        {
            var loans = _zonkyClient.GetAllPrimaryMarketPlaceAsync().GetAwaiter().GetResult();

            Assert.IsTrue(loans.Any());
        }
    }
}
