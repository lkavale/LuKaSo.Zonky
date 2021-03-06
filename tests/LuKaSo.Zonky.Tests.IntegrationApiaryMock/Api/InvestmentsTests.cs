﻿using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Markets;
using LuKaSo.Zonky.Tests.IntegrationApiaryMock.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "If method fails, exception will be thrown, so test not will not pass", Scope = "member", Target = "~M:LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api.InvestmentsTests.CreatePrimaryMarketInvestmentOk")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "If method fails, exception will be thrown, so test not will not pass", Scope = "member", Target = "~M:LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api.InvestmentsTests.InreasePrimaryMarketInvestmentOk")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "If method fails, exception will be thrown, so test not will not pass", Scope = "member", Target = "~M:LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api.InvestmentsTests.OfferInvestmentOnSecondaryMarketOk")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "If method fails, exception will be thrown, so test not will not pass", Scope = "member", Target = "~M:LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api.InvestmentsTests.CancelOfferInvestmentOnSecondaryMarketOk")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "If method fails, exception will be thrown, so test not will not pass", Scope = "member", Target = "~M:LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api.InvestmentsTests.BuyInvestmentOnSecondaryMarketOk")]

namespace LuKaSo.Zonky.Tests.IntegrationApiaryMock.Api
{
    [TestClass]
    public class InvestmentsTests
    {
        private ZonkyApi _zonkyApi;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = ZonkyApiFactory.Create();
            _tokenProvider = new AuthorizationTokenProvider(_zonkyApi);
        }

        [TestMethod]
        public void GetInvestmentsOk()
        {
            var pageSize = 10;
            var investments = _zonkyApi.GetInvestmentsAsync(0, pageSize, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            // Mock API returns no data
            Assert.IsNull(investments);
        }

        [TestMethod]
        public void GetInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetInvestmentsAsync(0, 10, token, CancellationToken.None));
        }

        [TestMethod]
        public void CreatePrimaryMarketInvestmentOk()
        {
            var investmentRequest = new PrimaryMarketInvestment()
            {
                Amount = 200,
                LoanId = 1,
                CaptchaResponse = "..."
            };

            _zonkyApi.CreatePrimaryMarketInvestmentAsync(investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void InreasePrimaryMarketInvestmentOk()
        {
            var investmentRequest = new IncreasePrimaryMarketInvestment()
            {
                Amount = 200
            };

            _zonkyApi.IncreasePrimaryMarketInvestmentAsync(1, investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void OfferInvestmentOnSecondaryMarketOk()
        {
            var request = new SecondaryMarketOfferSell()
            {
                InvestmentId = 15822,
                RemainingPrincipal = 199.66M,
                FeeAmount = 21.98M
            };

            _zonkyApi.OfferInvestmentOnSecondaryMarketAsync(request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CancelOfferInvestmentOnSecondaryMarketOk()
        {
            _zonkyApi.CancelOfferInvestmentOnSecondaryMarketAsync(123, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void BuyInvestmentOnSecondaryMarketOk()
        {
            var request = new SecondaryMarketInvestment()
            {
                Amount = 21.98M
            };

            _zonkyApi.BuySecondaryMarketInvestmentAsync(1, request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
