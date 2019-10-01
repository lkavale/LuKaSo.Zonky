using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Tests.IntegrationProduction.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Api
{
    [TestClass]
    public class LoansTests
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
        public void GetLoanOk()
        {
            var loan = _zonkyApi.GetLoanAsync(436639, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(436639, loan.Id);
            Assert.IsTrue(loan.Covered);
        }

        [TestMethod]
        public void GetLoanInvestmentsOk()
        {
            var loanInvestments = _zonkyApi.GetLoanInvestmentsAsync(436639, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreNotEqual(0, loanInvestments.Count());
            Assert.AreEqual(113, loanInvestments.Count());
        }

        [TestMethod]
        public void GetLoanInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetLoanInvestmentsAsync(436639, token, CancellationToken.None));
        }
    }
}
