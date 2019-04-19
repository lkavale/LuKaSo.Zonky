﻿using LuKaSo.Zonky.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    [TestClass]
    public class MarketplaceTests
    {
        private ZonkyApi _zonkyClient;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = ZonkyApiFactory.Create();
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, loans.Count());

            var loan = loans.First();

            Assert.AreEqual(1, loan.Id);
            Assert.AreEqual("zonky0", loan.NickName);
        }

        [TestMethod]
        public void GetSecondaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyClient.GetSecondaryMarketplaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}