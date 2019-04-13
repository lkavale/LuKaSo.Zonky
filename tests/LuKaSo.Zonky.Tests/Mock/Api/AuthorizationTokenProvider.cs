using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Api.Models.Login;
using System.Threading;

namespace LuKaSo.Zonky.Tests.Mock.Api
{
    public class AuthorizationTokenProvider
    {
        private ZonkyApi _zonkyApi;
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
