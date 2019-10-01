using LuKaSo.Zonky.Client;
using LuKaSo.Zonky.Tests.IntegrationProduction.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Client
{
    [TestClass]
    public class MarketplaceUnauthorizedTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            var zonkyLogin = new SecretsJsonReader().Read().LoginOk;
            _zonkyClient = new ZonkyClient(zonkyLogin, false, true);
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
            var loans = _zonkyClient.GetPrimaryMarketPlaceAsync(0, 10, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(loans.Any());
        }
    }
}
