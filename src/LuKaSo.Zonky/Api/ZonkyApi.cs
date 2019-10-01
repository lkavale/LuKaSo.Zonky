using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
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
        private JsonSerializerSettings Settings
        {
            get
            {
                return _settings.Value;
            }
        }

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
        }

        /// <summary>
        /// Zonky API destructor
        /// </summary>
        ~ZonkyApi()
        {
            Dispose(false);
        }

        /// <summary>
        /// Prepare paging and filter request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PreparePagingFilterRequest(string address, int page, int pageSize, FilterOptions filter = null)
        {
            return new ZonkyHttpRequestMessage(HttpMethod.Get, _baseUrl.Append(address))
                .AddFilterOptions(filter)
                .AddPaging(page, pageSize);
        }

        /// <summary>
        /// Prepare authorized paging and filter request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareAuthorizedRequest(string address, AuthorizationToken authorizationToken, int page, int pageSize, FilterOptions filter = null)
        {
            return PrepareAuthorizedRequest(address, authorizationToken)
                .AddFilterOptions(filter)
                .AddPaging(page, pageSize);
        }

        /// <summary>
        /// Prepare authorized request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="authorizationToken"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareAuthorizedRequest(string address, AuthorizationToken authorizationToken)
        {
            return PrepareAuthorizedRequest(address, HttpMethod.Get, authorizationToken)
                .AddBearerAuthorization(authorizationToken);
        }

        /// <summary>
        /// Prepare authorized request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="authorizationToken"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareAuthorizedRequest(string address, HttpMethod method, AuthorizationToken authorizationToken)
        {
            return new ZonkyHttpRequestMessage(method, _baseUrl.Append(address))
                .AddBearerAuthorization(authorizationToken);
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
                return JsonConvert.DeserializeObject<T>(responseData, Settings);
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
