using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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

        /// <summary>
        /// Extract data payload from message content and try to serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<T> ExtractDataAsync<T>(HttpResponseMessage response)
        {
            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                return JsonConvert.DeserializeObject<T>(responseData, _settings.Value);
            }
            catch (JsonException ex)
            {
                throw new BadResponceException(response, ex);
            }
        }

        /// <summary>
        /// Check authorization token
        /// </summary>
        /// <param name="authorizationToken"></param>
        public void CheckAuthorizationToken(AuthorizationToken authorizationToken)
        {
            if (authorizationToken == null)
            {
                throw new NotAuthorizedException();
            }
        }
    }
}
