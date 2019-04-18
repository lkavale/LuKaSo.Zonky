using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Client.Exceptions;
using LuKaSo.Zonky.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient
    {
        /// <summary>
        /// Check trading prerequisites
        /// </summary>
        private void CheckTradingPrerequisites()
        {
            if (!_enableTrading)
            {
                _log.Debug($"Zonky client could not make trading request due to disabled trading.");
                throw new TradingNotAllowedException();
            }
        }

        /// <summary>
        /// Check authorization prerequisites
        /// </summary>
        private void CheckAuthorizationPrerequisites()
        {
            if (_user == null)
            {
                _log.Debug($"Zonky client could not make authorized request due to disabled login.");
                throw new LoginNotAllowedException();
            }

            if (_wrongPassword)
            {
                _log.Debug($"Zonky client could not make authorized request for user {_user.Name} due to wrong password.");
                throw new BadLoginException(_user);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task LoginAsync(CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationPrerequisites();

            try
            {
                _log.Debug($"Client requests login user {_user.Name}.");
                _authorizationToken = await ZonkyApi.GetTokenExchangePasswordAsync(_user, ct).ConfigureAwait(false);
                _log.Debug($"User {_user.Name} logged in, new access token is {_authorizationToken.AccessToken.ToString()}, refresh token {_authorizationToken.RefreshToken.ToString()}.");

            }
            catch (BadLoginException ex)
            {
                _log.Error(ex, $"Client has wrong password for user {_user.Name}, another login tries are disabled.");
                _wrongPassword = true;

                throw;
            }
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task RefreshTokenAsync(CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationPrerequisites();

            try
            {
                if (_authorizationToken != null)
                {
                    _log.Debug($"Client requests refresh token for user {_user.Name}, refresh token {_authorizationToken.RefreshToken.ToString()}.");
                    _authorizationToken = await ZonkyApi.GetTokenExchangeRefreshTokenAsync(_authorizationToken, ct).ConfigureAwait(false);
                    _log.Debug($"Refresh token for user {_user.Name}, new access token is {_authorizationToken.AccessToken.ToString()}, refresh token {_authorizationToken.RefreshToken.ToString()}.");
                }
                else
                {
                    await LoginAsync(ct).ConfigureAwait(false);
                }
            }
            catch (BadRefreshTokenException ex)
            {
                _log.Info(ex, $"Client retrying login as {_user.Name} after unsuccessful refresh token request.");
                await LoginAsync(ct).ConfigureAwait(false);
            }
        }
    }
}
