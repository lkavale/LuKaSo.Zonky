using LuKaSo.Zonky.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LuKaSo.Zonky.Tests.Production.Long
{
    [TestClass]
    public class BusinessCodeTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            var zonkyLogin = new SecretsJsonReader().Read().LoginOk;
            _zonkyClient = new ZonkyClient(zonkyLogin, false);
        }

        //[TestMethod]
        public void AllInvestorEvents()
        {
            var dic = _zonkyClient.GetAllInvestmentsAsync()
                .GetAwaiter()
                .GetResult()
                .Select(i => new { Id = i.Id, Events = _zonkyClient.GetInvestmentEventsAsync(i.Id).GetAwaiter().GetResult() })
                .ToDictionary(i => i.Id, i => i.Events);
        }
    }
}
