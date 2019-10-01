using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Investor;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Overview;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi
    {
        /// <summary>
        /// Get investor wallet information
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Wallet> GetWalletAsync(AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/users/me/wallet", authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<Wallet>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/users/me/notifications", authorizationToken, page, pageSize))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<IEnumerable<Notification>>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="filter">Filter options</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/users/me/wallet/transactions", authorizationToken, page, pageSize))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<IEnumerable<WalletTransaction>>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }

        /// <summary>
        /// Get investor blocked amount
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/users/me/wallet/blocked-amounts", authorizationToken).AddSize(int.MaxValue))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<IEnumerable<BlockedAmount>>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<InvestorOverview> GetInvestorOverviewAsync(AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/statistics/me/overview", authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<InvestorOverview>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }
    }
}
