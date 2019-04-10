using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Tests.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading;

namespace LuKaSo.Zonky.Api.Tests
{
    [TestClass]
    public class LoansTests
    {
        private ZonkyApi _zonkyClient;
        private AuthorizationTokenProvider _tokenProvider;

        [TestInitialize]
        public void Init()
        {
            _zonkyClient = new ZonkyApi(new HttpClient());
            _tokenProvider = new AuthorizationTokenProvider(_zonkyClient);
        }

        [TestMethod]
        public void GetLoanOk()
        {
            var loan = _zonkyClient.GetLoanAsync(436639, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(436639, loan.Id);
        }

        [TestMethod]
        public void GetLoanInvestmentsOk()
        {
            var loanInvestments = _zonkyClient.GetLoanInvestmentsAsync(436639, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetLoanInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };
            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyClient.GetLoanInvestmentsAsync(436639, token, CancellationToken.None));
        }
    }
}
