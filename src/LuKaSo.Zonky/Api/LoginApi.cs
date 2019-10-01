using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models.Login;
using System;
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
        /// Get access token exchange with password
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct = default)
        {
            using (var request = PrepareLoginRequest(user))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                return await ExtractAuthorizationDataAsync(response, user).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get access token exchange with password and MFA token
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="code">MFA code</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangePasswordMfaAsync(User user, MfaCode code, CancellationToken ct = default)
        {
            using (var request = PrepareMfaLoginRequest(user, code))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                return await ExtractAuthorizationDataAsync(response, user).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get access token exchange with refresh token
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangeRefreshTokenAsync(AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareRefreshTokenRequest(authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                return await ExtractRefreshTokenDataAsync(response, authorizationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Prepare login request
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareLoginRequest(User user)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "username", user.Name },
                { "password", user.Password },
                { "grant_type", GrantType.password.ToString() },
                { "scope", AuthorizationScope.SCOPE_APP_WEB.ToString()}
            };

            return PrepareAuthorizationRequest(parameters);
        }

        /// <summary>
        /// Prepare MFA login request
        /// </summary>
        /// <param name="user"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareMfaLoginRequest(User user, MfaCode code)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "mfa_token", code.MfaToken.ToString() },
                { "sms_code", code.SmsCode.ToString() }
            };

            return PrepareLoginRequest(user)
                .AddQueryParameters(parameters);
        }

        /// <summary>
        /// Prepare refresh token request
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareRefreshTokenRequest(AuthorizationToken authorizationToken)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "refresh_token", authorizationToken.RefreshToken.ToString() },
                { "grant_type", GrantType.refresh_token.ToString() },
                { "scope", AuthorizationScope.SCOPE_APP_WEB.ToString()}
            };

            return PrepareAuthorizationRequest(parameters);
        }

        /// <summary>
        /// Prepare authorization request with parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private ZonkyHttpRequestMessage PrepareAuthorizationRequest(IReadOnlyDictionary<string, string> parameters)
        {
            return new ZonkyHttpRequestMessage(HttpMethod.Post, _baseUrl.Append("oauth/token"))
                .AddBasicAuthorization(_oAuth2Secret)
                .AddQueryParameters(parameters);
        }

        /// <summary>
        /// Extract authorization data
        /// </summary>
        /// <param name="response"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<AuthorizationToken> ExtractAuthorizationDataAsync(HttpResponseMessage response, User user)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                case HttpStatusCode.BadRequest:
                    await ExtractAuthorizationErrorAsync(response, user).ConfigureAwait(false);
                    throw new BadResponceException(response);
                default:
                    throw new ServerErrorException(response);
            }
        }

        /// <summary>
        /// Extract authorization error
        /// </summary>
        /// <param name="response"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task ExtractAuthorizationErrorAsync(HttpResponseMessage response, User user)
        {
            var error = await ExtractDataAsync<AuthorizationError>(response).ConfigureAwait(false);

            switch (error.Code)
            {
                case AuthorizationErrorCode.MFA_REQUIRED:
                    throw new MultiFactorAuthenticationRequiredException((Guid)error.MfaToken);
                case AuthorizationErrorCode.CAPTCHA_REQUIRED:
                    throw new BadLoginException(response, user);
            }
        }

        /// <summary>
        /// Extract refresh token data
        /// </summary>
        /// <param name="response"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<AuthorizationToken> ExtractRefreshTokenDataAsync(HttpResponseMessage response, AuthorizationToken token)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                case HttpStatusCode.BadRequest:
                    throw new BadRefreshTokenException(response, token);
                default:
                    throw new ServerErrorException(response);
            }
        }
    }
}