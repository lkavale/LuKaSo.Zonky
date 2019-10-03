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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await _resolverFactory.Create<AuthorizationToken>(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK)
                    .ConfigureStatusResponce<AuthorizationError>(HttpStatusCode.BadRequest, (error, message) => HandleAuthorizationError(error, message, user))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await _resolverFactory.Create<AuthorizationToken>(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK)
                    .ConfigureStatusResponce<AuthorizationError>(HttpStatusCode.BadRequest, (error, message) => HandleAuthorizationError(error, message, user))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await _resolverFactory.Create<AuthorizationToken>(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK)
                    .ConfigureStatusResponce(HttpStatusCode.BadRequest, (message) => throw new BadRefreshTokenException(message, authorizationToken))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
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
        /// Handle authorization error
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private void HandleAuthorizationError(AuthorizationError error, HttpResponseMessage message, User user)
        {
            switch (error.Code)
            {
                case AuthorizationErrorCode.MFA_REQUIRED:
                    throw new MultiFactorAuthenticationRequiredException((Guid)error.MfaToken);
                case AuthorizationErrorCode.CAPTCHA_REQUIRED:
                    throw new BadLoginException(message, user);
                default:
                    throw new BadResponceException(message);

            }
        }
    }
}