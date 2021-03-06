﻿using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Tests.IntegrationApiaryMock.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api
{
    [TestClass]
    public class MarketplaceTests
    {
        private ZonkyApi _zonkyApi;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = ZonkyApiFactory.Create();
        }

        [TestMethod]
        public void GetPrimaryMarketplaceOk()
        {
#warning In the result in insuranceHistory field is bad insurance history item value, test temporarily disabled
            /*
            var pageSize = 10;

            var loans = _zonkyApi.GetPrimaryMarketPlaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, loans.Count());

            var loan = loans.First();

            Assert.AreEqual(1, loan.Id);
            Assert.AreEqual("zonky0", loan.NickName);
            */
        }

        [TestMethod]
        public void GetSecondaryMarketplaceOk()
        {
            var pageSize = 10;
            var loans = _zonkyApi.GetSecondaryMarketplaceAsync(0, pageSize, null, CancellationToken.None).GetAwaiter().GetResult();

            // Mock API returns no data
            Assert.IsNull(loans);
        }
    }
}
