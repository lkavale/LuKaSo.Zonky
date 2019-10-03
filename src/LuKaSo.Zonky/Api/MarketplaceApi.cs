using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Markets;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default)
        {
            using (var request = PreparePrimaryMarketplaceRequest(page, pageSize, filter))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<Loan>>(response);
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
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PreparePrimaryMarketplaceRequest(page, pageSize, filter).AddBearerAuthorization(authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<Loan>>(response, true);
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
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default)
        {
            using (var request = PrepareSecondaryMarketplaceRequest(page, pageSize, filter))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<SecondaryMarketOffer>>(response);
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
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareSecondaryMarketplaceRequest(page, pageSize, filter).AddBearerAuthorization(authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<SecondaryMarketOffer>>(response, true);
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
            return PreparePagingFilterRequest("/smp/investments", page, pageSize, filter);
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
            return PreparePagingFilterRequest("/loans/marketplace", page, pageSize, filter);
        }
    }
}
