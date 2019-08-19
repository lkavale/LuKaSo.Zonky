using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Logging;
using LuKaSo.Zonky.Models.Login;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi : IZonkyApi
    {
        /// <summary>
        /// Zonky API base address
        /// </summary>
        private readonly Uri _baseUrl;

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
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Zonky API constructor with default production address of service
        /// </summary>
        /// <param name="httpClient">HTTP client</param>
        public ZonkyApi(HttpClient httpClient) : this(new Uri("https://api.zonky.cz/"), httpClient)
        {
        }

        /// <summary>
        /// Zonky API constructor
        /// </summary>
        /// <param name="baseUrl">Base URL of service</param>
        /// <param name="httpClient">HTTP client</param>
        public ZonkyApi(Uri baseUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                return settings;
            });
            _log = LogProvider.For<ZonkyApi>();
        }

        /// <summary>
        /// Zonky API destructor
        /// </summary>
        ~ZonkyApi()
        {
            Dispose(false);
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

        /// <summary>
        /// Check responce for authorization errors
        /// </summary>
        /// <param name="response"></param>
        protected void CheckAuthorizedResponce(HttpResponseMessage response)
        {
            if (response != null && response.Headers != null &&
                (response.StatusCode == HttpStatusCode.Unauthorized ||
                (response.Headers.TryGetValues("WWW-Authenticate", out var authHeader) &&
                authHeader.Any(s => s.Contains("Bearer error=\"invalid_token\"")))))
            {
                throw new BadAccessTokenException();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }
    }
}
