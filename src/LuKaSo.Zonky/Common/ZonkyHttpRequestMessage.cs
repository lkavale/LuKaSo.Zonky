using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LuKaSo.Zonky.Common
{
    public class ZonkyHttpRequestMessage : HttpRequestMessage
    {
        public ZonkyHttpRequestMessage(HttpMethod method, Uri address) : base(method, address)
        {
            Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
        }

        /// <summary>
        /// Add paging to request
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestPaging(int page, int pageSize)
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
        public ZonkyHttpRequestMessage AddRequestBearerAuthorization(AuthorizationToken authorizationToken)
        {
            Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

            return this;
        }

        /// <summary>
        /// Add basic authorization to request
        /// </summary>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestBasicAuthorization(string secret)
        {
            Headers.Authorization = new AuthenticationHeaderValue("Basic", secret);

            return this;
        }

        /// <summary>
        /// Add filter to request
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestFilter(FilterOptions filter)
        {
            RequestUri = RequestUri.AppendFilterOptions(filter);

            return this;
        }

        /// <summary>
        /// Add query parameters
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestParameters(IReadOnlyDictionary<string, string> parameters)
        {
            RequestUri = RequestUri.AttachQueryParameters(parameters);

            return this;
        }
    }
}
