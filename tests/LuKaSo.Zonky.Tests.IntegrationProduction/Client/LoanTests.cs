using LuKaSo.Zonky.Client;
using LuKaSo.Zonky.Tests.IntegrationProduction.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Client
{
    [TestClass]
    public class LoanTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            var zonkyLogin = new SecretsJsonReader().Read().LoginOk;
            _zonkyClient = new ZonkyClient(zonkyLogin, false);
        }

        [TestMethod]
        public void GetLoanInvestmentsOk()
        {
            var loanParticipations = _zonkyClient.GetLoanInvestmentsAsync(360994, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(163, loanParticipations.Count());
        }
    }
}
