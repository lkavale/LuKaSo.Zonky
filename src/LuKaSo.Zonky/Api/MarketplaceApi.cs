using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Markets;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi
    {
        /// <summary>
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            using (var request = PreparePrimaryMarketplaceRequest(page, pageSize, filter))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                return await ExtractMarketplaceDataAsync<Loan>(response);
            }
        }

        /// <summary>
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="authorizationToken">Authorization token</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PreparePrimaryMarketplaceRequest(page, pageSize, filter).AddRequestAuthorization(authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);
                return await ExtractMarketplaceDataAsync<Loan>(response);
            }
        }

        /// <summary>
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            using (var request = PrepareSecondaryMarketplaceRequest(page, pageSize, filter))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                return await ExtractMarketplaceDataAsync<SecondaryMarketOffer>(response);
            }
        }

        /// <summary>
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="authorizationToken">Authorization token</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareSecondaryMarketplaceRequest(page, pageSize, filter).AddRequestAuthorization(authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);
                return await ExtractMarketplaceDataAsync<SecondaryMarketOffer>(response);
            }
        }

        /// <summary>
        /// Prepare secondary marketplace request
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareSecondaryMarketplaceRequest(int page, int pageSize, FilterOptions filter = null)
        {
            return PrepareMarketplaceRequest("/smp/investments", page, pageSize, filter);
        }

        /// <summary>
        /// Prepare primary marketplace request
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PreparePrimaryMarketplaceRequest(int page, int pageSize, FilterOptions filter = null)
        {
            return PrepareMarketplaceRequest("/loans/marketplace", page, pageSize, filter);
        }

        /// <summary>
        /// Prepare marketplace request
        /// </summary>
        /// <param name="address"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareMarketplaceRequest(string address, int page, int pageSize, FilterOptions filter = null)
        {
            return new ZonkyHttpRequestMessage(new HttpMethod("GET"), _baseUrl.Append(address))
                .AddRequestFilter(filter)
                .AddRequestPaging(page, pageSize);
        }

        /// <summary>
        /// Extract marketplace data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IEnumerable<T>> ExtractMarketplaceDataAsync<T>(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await ExtractDataAsync<IEnumerable<T>>(response).ConfigureAwait(false);
                default:
                    throw new ServerErrorException(response);
            }
        }
    }
}
