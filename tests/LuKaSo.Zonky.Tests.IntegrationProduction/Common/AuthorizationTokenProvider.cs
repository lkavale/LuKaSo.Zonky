﻿using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Models.Login;
using System.Threading;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Common
{
    public class AuthorizationTokenProvider
    {
        private readonly ZonkyApi _zonkyApi;
        private readonly User _zonkyLogin;

        public AuthorizationTokenProvider(ZonkyApi zonkyApi)
        {
            _zonkyApi = zonkyApi;
            _zonkyLogin = new SecretsJsonReader().Read().LoginOk;
        }

        public AuthorizationToken GetToken()
        {
            return _zonkyApi.GetTokenExchangePasswordAsync(_zonkyLogin, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
