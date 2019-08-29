using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Login;
using System;
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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestPaging(int page, int pageSize)
        {
            Headers.Add("x-page", page.ToString());
            Headers.Add("x-size", pageSize.ToString());

            return this;
        }

        /// <summary>
        /// Add authorization to request
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestAuthorization(AuthorizationToken authorizationToken)
        {
            Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

            return this;
        }

        /// <summary>
        /// Add filter to request
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ZonkyHttpRequestMessage AddRequestFilter(FilterOptions filter)
        {
            RequestUri = RequestUri.AppendFilterOptions(filter);

            return this;
        }
    }
}
