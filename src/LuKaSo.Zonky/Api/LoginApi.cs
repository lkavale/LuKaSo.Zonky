using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
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
        /// Get access token exchange with password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct)
        {
            var url = _baseUrl
                .Append("oauth/token")
                .AttachQueryParameters(new Dictionary<string, string>() {
                    { "username", user.Name },
                    { "password", user.Password },
                    { "grant_type", GrantType.password.ToString() },
                    { "scope", AuthorizationScope.SCOPE_APP_WEB.ToString()} });

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _oAuth2Secret);

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
                            return JsonConvert.DeserializeObject<AuthorizationToken>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new BadLoginException(response, user);
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
        /// Get access token exchange with refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangeRefreshTokenAsync(AuthorizationToken token, CancellationToken ct)
        {
            var url = _baseUrl
                .Append("oauth/token")
                .AttachQueryParameters(new Dictionary<string, string>() {
                    { "refresh_token", token.RefreshToken.ToString() },
                    { "grant_type", GrantType.refresh_token.ToString() },
                    { "scope", AuthorizationScope.SCOPE_APP_WEB.ToString()} });

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _oAuth2Secret);

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
                            return JsonConvert.DeserializeObject<AuthorizationToken>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new BadRefreshTokenException(response, token);
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
