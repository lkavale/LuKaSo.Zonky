using LuKaSo.Zonky.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Production.Client
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
