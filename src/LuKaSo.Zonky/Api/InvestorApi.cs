﻿using LuKaSo.Zonky.Exceptions;
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
        public async Task<Wallet> GetWalletAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<Wallet>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw PrepareBadRequestException(response, new ServerErrorException(response));
                        default:
                            throw new ServerErrorException(response);
                    }
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
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/notifications");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());
                request.Headers.Add("x-page", page.ToString());
                request.Headers.Add("x-size", pageSize.ToString());

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<Notification>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw PrepareBadRequestException(response, new ServerErrorException(response));
                        default:
                            throw new ServerErrorException(response);
                    }
                }
            }
        }

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet/transactions").AppendFilterOptions(filter);
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());
                request.Headers.Add("x-size", "2147483647");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<WalletTransaction>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw PrepareBadRequestException(response, new ServerErrorException(response));
                        default:
                            throw new ServerErrorException(response);
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
        public async Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/wallet/blocked-amounts");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());
                request.Headers.Add("x-size", "2147483647");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<BlockedAmount>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw PrepareBadRequestException(response, new ServerErrorException(response));
                        default:
                            throw new ServerErrorException(response);
                    }
                }
            }
        }

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<InvestorOverview> GetInvestorOverviewAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/statistics/me/overview");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<InvestorOverview>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
            }
        }
    }
}
