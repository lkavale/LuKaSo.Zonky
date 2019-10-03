using LuKaSo.Zonky.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Common
{
    public class ZonkyResponseResolver<T>
    {
        private readonly JsonSerializerSettings _settings;
        private readonly bool _isAuthorizedRequest;
        private readonly Dictionary<HttpStatusCode, Func<HttpResponseMessage, Task<T>>> _responces;

        /// <summary>
        /// Zonky response resolver
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="isAuthorizedRequest"></param>
        public ZonkyResponseResolver(JsonSerializerSettings settings, bool isAuthorizedRequest = false)
        {
            _settings = settings;
            _isAuthorizedRequest = isAuthorizedRequest;
            _responces = new Dictionary<HttpStatusCode, Func<HttpResponseMessage, Task<T>>>();
        }

        /// <summary>
        /// Configure response on returned HTTP status code
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public ZonkyResponseResolver<T> ConfigureStatusResponce(HttpStatusCode statusCode)
        {
            _responces.Add(statusCode, async (response) => await ExtractContentDataAsync<T>(response).ConfigureAwait(false));

            return this;
        }

        /// <summary>
        /// Configure response on returned HTTP status code
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public ZonkyResponseResolver<T> ConfigureStatusResponce(HttpStatusCode statusCode, Action<HttpResponseMessage> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _responces.Add(statusCode, async (response) =>
            {
                action(response);
                return default;
            });

            return this;
        }

        /// <summary>
        /// Configure response on returned HTTP status code
        /// </summary>
        /// <typeparam name="TExtract"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public ZonkyResponseResolver<T> ConfigureStatusResponce<TExtract>(HttpStatusCode statusCode, Action<TExtract, HttpResponseMessage> function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            ConfigureStatusResponce(statusCode, async (response) =>
            {
                var data = await ExtractContentDataAsync<TExtract>(response).ConfigureAwait(false);
                function(data, response);
            });

            return this;
        }

        /// <summary>
        /// Configure response as deault
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public ZonkyResponseResolver<T> ConfigureDefaultResponce(Func<HttpResponseMessage, Task<T>> function)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            _responces.Add(0, function);

            return this;
        }

        /// <summary>
        /// Extract data from response message
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public virtual async Task<T> ExtractDataAsync(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
                throw new ArgumentNullException(nameof(responseMessage));

            if (_isAuthorizedRequest)
                CheckAuthorizedResponce(responseMessage);

            if (!_responces.ContainsKey(responseMessage.StatusCode))
                return await _responces[0](responseMessage);

            return await _responces[responseMessage.StatusCode](responseMessage);
        }

        /// <summary>
        /// Extract data payload from message content and try to serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        protected async Task<TData> ExtractContentDataAsync<TData>(HttpResponseMessage responseMessage)
        {
            var responseData = responseMessage.Content == null ? null : await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                return JsonConvert.DeserializeObject<TData>(responseData, _settings);
            }
            catch (JsonException ex)
            {
                throw new BadResponceException(responseMessage, ex);
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
    }
}
