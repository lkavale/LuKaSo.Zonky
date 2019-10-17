using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using Newtonsoft.Json;
using System;
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
        private readonly string _oAuth2Secret;

        /// <summary>
        /// Used HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Responce resolver factory
        /// </summary>
        private readonly ZonkyResponseResolverFactory _resolverFactory;

        /// <summary>
        /// JSON serializer settings
        /// </summary>
        private readonly Lazy<JsonSerializerSettings> _settings;
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
        public ZonkyApi(string oAuth2Secret, HttpClient httpClient) : this(new Uri("https://api.zonky.cz/"), oAuth2Secret, httpClient)
        {
        }

        /// <summary>
        /// Zonky API constructor
        /// </summary>
        /// <param name="baseUrl">Base URL of service</param>
        /// <param name="httpClient">HTTP client</param>
        public ZonkyApi(Uri baseUrl, string oauth2Secret, HttpClient httpClient) : this(baseUrl, oauth2Secret, httpClient, new ZonkyResponseResolverFactory())
        {
        }

        /// <summary>
        /// Zonky API constructor
        /// </summary>
        /// <param name="baseUrl">Base URL of service</param>
        /// <param name="httpClient">HTTP client</param>
        /// <param name="resolverFactory">Resolver factory</param>
        internal ZonkyApi(Uri baseUrl, string oauth2Secret, HttpClient httpClient, ZonkyResponseResolverFactory resolverFactory)
        {
            _httpClient = httpClient;
            _oAuth2Secret = oauth2Secret;
            _baseUrl = baseUrl;
            _settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                return settings;
            });
            _resolverFactory = resolverFactory;
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
        /// Extract responce data with HTTP 200, other status codes are common errors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responce"></param>
        /// <param name="isAuthorized"></param>
        /// <returns></returns>
        private async Task<T> ExtractResponceOkErrorDataAsync<T>(HttpResponseMessage responce, bool isAuthorized = false)
        {
            return await _resolverFactory.Create<T>(Settings, isAuthorized)
                .ConfigureStatusResponce(HttpStatusCode.OK)
                .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                .ExtractDataAsync(responce);
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
