using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    [TestClass]
    public class LoansTests
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
        public void GetLoanOk()
        {
#warning In the result in insuranceHistory field is bad insurance history item value, test temporarily disabled
            /*
            var loan = _zonkyApi.GetLoanAsync(436639, CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreEqual(1, loan.Id);
            Assert.AreEqual("zonky0", loan.NickName);
            */
        }

        [TestMethod]
        public void GetLoanInvestmentsOk()
        {
            var loanInvestments = _zonkyApi.GetLoanInvestmentsAsync(1, _tokenProvider.GetToken(), CancellationToken.None).GetAwaiter().GetResult();

            Assert.AreNotEqual(0, loanInvestments.Count());
            Assert.AreEqual(2, loanInvestments.Count());
        }

        [TestMethod]
        public void GetLoanInvestmentsNotAuthorized()
        {
            var token = new AuthorizationToken() { AccessToken = Guid.NewGuid() };

            Assert.ThrowsExceptionAsync<NotAuthorizedException>(() => _zonkyApi.GetLoanInvestmentsAsync(436639, token, CancellationToken.None));
        }
    }
}
