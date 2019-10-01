using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LuKaSo.Zonky.Common
{
    internal class ZonkyHttpRequestMessage : HttpRequestMessage
    {
        public ZonkyHttpRequestMessage(HttpMethod method, Uri address) : base(method, address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        }

        /// <summary>
        /// Add size to request
        /// </summary>
        /// <param name="size">Size</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddSize(int size)
        {
            Headers.Add("x-size", size.ToString());

            return this;
        }

        /// <summary>
        /// Add paging to request
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddPaging(int page, int pageSize)
        {
            Headers.Add("x-page", page.ToString());
            Headers.Add("x-size", pageSize.ToString());

            return this;
        }

        /// <summary>
        /// Add bearer authorization to request
        /// </summary>
        /// <param name="authorizationToken">Authorization token</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddBearerAuthorization(AuthorizationToken authorizationToken)
        {
            if (authorizationToken == null)
            {
                throw new ArgumentNullException(nameof(authorizationToken));
            }

            Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

            return this;
        }

        /// <summary>
        /// Add basic authorization to request
        /// </summary>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddBasicAuthorization(string secret)
        {
            Headers.Authorization = new AuthenticationHeaderValue("Basic", secret);

            return this;
        }

        /// <summary>
        /// Add filter to request
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddFilterOptions(FilterOptions filter = null)
        {
            if (filter != null)
            {
                RequestUri = RequestUri.AppendFilterOptions(filter);
            }

            return this;
        }

        /// <summary>
        /// Add query parameters to request
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddQueryParameters(IReadOnlyDictionary<string, string> parameters)
        {
            RequestUri = RequestUri.AttachQueryParameters(parameters);

            return this;
        }

        /// <summary>
        /// Add JSON serialized object to request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddJsonContent<T>(T @object, JsonSerializerSettings settings)
        {
            Content = new StringContent(JsonConvert.SerializeObject(@object, settings), Encoding.UTF8, "application/json");

            return this;
        }
    }
}
