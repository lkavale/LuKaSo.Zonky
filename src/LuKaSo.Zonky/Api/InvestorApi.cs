using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Investor;
using LuKaSo.Zonky.Api.Models.Login;
using Newtonsoft.Json;
using System;
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
            var url = _baseUrl.Append("/users/me/wallet");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<Wallet>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
            var url = _baseUrl.Append("/users/me/notifications");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<Notification>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
            var url = _baseUrl.Append("/users/me/wallet/transactions");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<WalletTransaction>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
            var url = _baseUrl.Append("/users/me/wallet/blocked-amounts");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<BlockedAmount>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
