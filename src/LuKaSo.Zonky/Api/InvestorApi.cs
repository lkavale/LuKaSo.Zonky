using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Investor;
using LuKaSo.Zonky.Api.Models.Login;
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
        public async Task<Wallet> GetWalletAsync(AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<Wallet>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/notifications");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<Notification>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet/transactions");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<WalletTransaction>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Get investor blocked amount
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet/blocked-amounts");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<BlockedAmount>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }
    }
}
