using LuKaSo.Zonky.Client;
using LuKaSo.Zonky.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

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
        [TestCategory("Full")]
        public void GetAllNotificationsAsyncOk()
        {
            var notifications = _zonkyClient.GetAllNotificationsAsync().GetAwaiter().GetResult();

            Assert.IsTrue(notifications.Any());
        }

        [TestMethod]
        public void GetAllWalletTransactionsAsynLongTermOk()
        {
            var date = DateTime.Now.AddDays(-10);
            var filter = new FilterOptions();
            filter.Add("transaction.transactionDate__gte", $"{date.Year}-{date.Month}-{date.Day}");

            var walletTransactions = _zonkyClient.GetAllWalletTransactionsAsync(filter, CancellationToken.None).GetAwaiter().GetResult();

            Assert.IsTrue(walletTransactions.Any());
        }

        [TestMethod]
        [TestCategory("Full")]
        public void GetAllWalletTransactionsAsynOk()
        {
            var walletTransactions = _zonkyClient.GetAllWalletTransactionsAsync().GetAwaiter().GetResult();

            Assert.IsTrue(walletTransactions.Any());
        }
    }
}
