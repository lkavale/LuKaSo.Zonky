using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Production.Api
{
    [TestClass]
    public class InvestmentsTests
    {
        private ZonkyApi _zonkyApi;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyApi = new ZonkyApi(new HttpClient());
            _tokenProvider = new AuthorizationTokenProvider(_zonkyApi);
        }

        [TestMethod]
        public void GetInvestmentsOk()
        {
            var pageSize = 10;
            var investments = _zonkyApi.GetInvestmentsAsync(0, pageSize, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(pageSize, investments.Count());
        }

        [TestMethod]
        public void GetInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetInvestmentsAsync(0, 10, token, CancellationToken.None));
        }

        [TestMethod]
        public void GetInvestmentEventsOk()
        {
            var loanId = 115665;
            var investmentEvents = _zonkyApi.GetInvestmentEventsAsync(loanId, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(investmentEvents.Count(), investmentEvents.Count(e => e.LoanId == loanId));
        }

        /*
        [TestMethod]
        public void GetInvestmentEventsAllOk()
        {
            var investments = _zonkyClient.GetInvestmentsAsync(0, 10000, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            foreach (var investment in investments)
            {
                var investmentEvents = _zonkyClient.GetInvestmentEventsAsync(investment.LoanId, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
                Assert.AreEqual(investmentEvents.Count(), investmentEvents.Where(e => e.LoanId == investment.LoanId).Count());
            }
        }
        */

        [TestMethod]
        public void GetInvestmentEventsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetInvestmentEventsAsync(115665, token, CancellationToken.None));
        }
    }
}
