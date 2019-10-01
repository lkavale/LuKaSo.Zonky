using LuKaSo.Zonky.Client;
using LuKaSo.Zonky.Tests.IntegrationProduction.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Client
{
    [TestClass]
    public class InvestmentsTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            var zonkyLogin = new SecretsJsonReader().Read().LoginOk;
            _zonkyClient = new ZonkyClient(zonkyLogin, false);
        }

        [TestMethod]
        [TestCategory("Full")]
        public void AllInvestorEvents()
        {
            var investmentsEvents = _zonkyClient.GetAllInvestmentsAsync()
                .GetAwaiter()
                .GetResult()
                .Select(i => _zonkyClient.GetInvestmentEventsAsync(i.LoanId).GetAwaiter().GetResult());

            Assert.IsTrue(investmentsEvents.Any(i => i.Any()));
        }
    }
}
