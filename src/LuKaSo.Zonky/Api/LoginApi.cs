using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models.Login;
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
        public async Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct = default(CancellationToken))
        {
            using (var request = PrepareLoginRequest(user))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                    case HttpStatusCode.BadRequest:
                        await HandleAuthorizationErrorAsync(response, user);
                        throw new BadResponceException(response);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }

        /// <summary>
        /// Get access token exchange with password and MFA token
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="code">MFA code</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AuthorizationToken> GetTokenExchangePasswordMfaAsync(User user, MfaCode code, CancellationToken ct = default(CancellationToken))
        {
            using (var request = PrepareMfaLoginRequest(user, code))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<AuthorizationToken>(response).ConfigureAwait(false);
                    case HttpStatusCode.BadRequest:
                        await HandleAuthorizationErrorAsync(response, user);
                        throw new BadResponceException(response);
                    default:
                        throw new ServerErrorException(response);
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

            using (var request = PrepareRefreshTokenRequest(authorizationToken))
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

        private ZonkyHttpRequestMessage PrepareMfaLoginRequest(User user, MfaCode code)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "mfa_token", code.MfaToken.ToString() },
                { "sms_code", code.SmsCode.ToString() }
            };

            return PrepareLoginRequest(user)
                .AddRequestParameters(parameters);
        }

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

        private ZonkyHttpRequestMessage PrepareAuthorizationRequest(IReadOnlyDictionary<string, string> parameters)
        {
            return new ZonkyHttpRequestMessage(new HttpMethod("POST"), _baseUrl.Append("oauth/token"))
                .AddRequestBasicAuthorization(_oAuth2Secret)
                .AddRequestParameters(parameters);
        }

        private async Task HandleAuthorizationErrorAsync(HttpResponseMessage response, User user)
        {
            var error = await ExtractDataAsync<AuthorizationError>(response).ConfigureAwait(false);

            switch (error.Code)
            {
                case AuthorizationErrorCode.MFA_REQUIRED:
                    throw new MultiFactorAuthenticationRequiredException();
                case AuthorizationErrorCode.CAPTCHA_REQUIRED:
                    throw new BadLoginException(response, user);
            }

            throw new BadResponceException(response);
        }
    }
}