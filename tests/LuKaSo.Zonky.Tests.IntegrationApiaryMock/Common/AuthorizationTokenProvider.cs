using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Models.Login;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationApiaryMock.Common
{
    public class AuthorizationTokenProvider
    {
        private readonly ZonkyApi _zonkyApi;
        private readonly User _zonkyLogin;

        public AuthorizationTokenProvider(ZonkyApi zonkyApi)
        {
            _zonkyApi = zonkyApi;
            _zonkyLogin = new User("test", "test");
        }

        public AuthorizationToken GetToken()
        {
            return _zonkyApi.GetTokenExchangePasswordAsync(_zonkyLogin, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
