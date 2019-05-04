using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
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
        /// Get access token exchange with password
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct = default(CancellationToken))
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

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                        case HttpStatusCode.BadRequest:
                            throw new BadLoginException(response, user);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
            }
        }

        /// <summary>
        /// Get access token exchange with refresh token
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangeRefreshTokenAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationToken(authorizationToken);

            var url = _baseUrl
                .Append("oauth/token")
                .AttachQueryParameters(new Dictionary<string, string>() {
                    { "refresh_token", authorizationToken.RefreshToken.ToString() },
                    { "grant_type", GrantType.refresh_token.ToString() },
                    { "scope", AuthorizationScope.SCOPE_APP_WEB.ToString()} });

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("POST");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _oAuth2Secret);

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                        case HttpStatusCode.BadRequest:
                            throw new BadRefreshTokenException(response, authorizationToken);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
            }
        }
    }
}