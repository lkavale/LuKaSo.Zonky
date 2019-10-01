using LuKaSo.Zonky.Api;
using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Tests.IntegrationApiaryMock.Common
{
    internal static class ZonkyApiFactory
    {
        public static ZonkyApi Create()
        {
            return new ZonkyApi(new Uri("http://private-4c0953-zonky.apiary-mock.com"), new HttpClient());
        }
    }
}
