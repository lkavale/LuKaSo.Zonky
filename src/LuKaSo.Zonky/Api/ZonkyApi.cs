using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi : IZonkyApi
    {
        /// <summary>
        /// Zonky API base address
        /// </summary>
        private readonly Uri _baseUrl = new Uri("https://api.zonky.cz/");

        /// <summary>
        /// OAuth preshared secret
        /// </summary>
        private const string _oAuth2Secret = "d2ViOndlYg==";

        /// <summary>
        /// Used HTTP client
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// JSON serializer settings
        /// </summary>
        private Lazy<JsonSerializerSettings> _settings;

        /// <summary>
        /// Zonky API constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public ZonkyApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                return settings;
            });
        }
    }
}
