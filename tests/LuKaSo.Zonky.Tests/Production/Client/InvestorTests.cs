using LuKaSo.Zonky.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LuKaSo.Zonky.Tests.Production.Client
{
    [TestClass]
    public class InvestorTests
    {
        private ZonkyClient _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            var zonkyLogin = new SecretsJsonReader().Read().LoginOk;
            _zonkyClient = new ZonkyClient(zonkyLogin, false);
        }

        [TestMethod]
        public void GetAllNotificationsOk()
        {
            var notifications = _zonkyClient.GetAllNotificationsAsync().GetAwaiter().GetResult();

            Assert.IsTrue(notifications.Any());
        }
    }
}
