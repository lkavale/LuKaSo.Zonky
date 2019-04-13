using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Api.Models.Markets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    [TestClass]
    public class InvestmentsTests
    {
        private ZonkyApi _zonkyClient;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = ZonkyApiFactory.Create();
            _tokenProvider = new AuthorizationTokenProvider(_zonkyClient);
        }

        [TestMethod]
        public void GetInvestmentsOk()
        {
            var pageSize = 10;
            var investments = _zonkyClient.GetInvestmentsAsync(0, pageSize, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetInvestmentsAsync(0, 10, token, CancellationToken.None));
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

            _zonkyClient.CreatePrimaryMarketInvestmentAsync(investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CreatePrimaryMarketInvestmentNotAuthorized()
        {
            var investmentRequest = new PrimaryMarketInvestment()
            {
                Amount = 200,
                LoanId = 1,
                CaptchaResponse = "..."
            };

            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            _zonkyClient.CreatePrimaryMarketInvestmentAsync(investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void InreasePrimaryMarketInvestmentOk()
        {
            var investmentRequest = new IncreasePrimaryMarketInvestment()
            {
                Amount = 200
            };

            _zonkyClient.IncreasePrimaryMarketInvestmentAsync(1, investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void InreasePrimaryMarketInvestmentNotAuthorized()
        {
            var investmentRequest = new IncreasePrimaryMarketInvestment()
            {
                Amount = 200
            };

            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            _zonkyClient.IncreasePrimaryMarketInvestmentAsync(1, investmentRequest, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
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

            _zonkyClient.OfferInvestmentOnSecondaryMarketAsync(request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void OfferInvestmentOnSecondaryMarketNotAuthorized()
        {
            var request = new SecondaryMarketOfferSell()
            {
                InvestmentId = 15822,
                RemainingPrincipal = 199.66M,
                FeeAmount = 21.98M
            };

            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            _zonkyClient.OfferInvestmentOnSecondaryMarketAsync(request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CancelOfferInvestmentOnSecondaryMarketOk()
        {
            _zonkyClient.CancelOfferInvestmentOnSecondaryMarketAsync(123, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CancelOfferInvestmentOnSecondaryMarketNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            _zonkyClient.CancelOfferInvestmentOnSecondaryMarketAsync(123, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void BuyInvestmentOnSecondaryMarketOk()
        {
            var request = new SecondaryMarketInvestment()
            {
                Amount = 21.98M
            };

            _zonkyClient.BuySecondaryMarketInvestmentAsync(1, request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void BuyInvestmentOnSecondaryMarketNotAuthorized()
        {
            var request = new SecondaryMarketInvestment()
            {
                Amount = 21.98M
            };

            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            _zonkyClient.BuySecondaryMarketInvestmentAsync(1, request, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
