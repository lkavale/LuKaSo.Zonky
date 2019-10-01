using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Logging;
using LuKaSo.Zonky.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient : IZonkyClient
    {
        /// <summary>
        /// Max paging size, requests for lower item amounts (lower thousands of items, eg. primary market, large investment portfolio > 1000 participations, etc.)
        /// failing on timeouts very often, so it is better to split the request to many 
        /// </summary>
        private const int _maxPageSize = 500;

        /// <summary>
        /// Max paging size, requests for huge item amounts (thousands of items, eg. wallet transactions, etc.)
        /// failing on timeouts very often, so it is better to split the request to many 
        /// </summary>
        private const int _maxLargePageSize = 10000;

        /// <summary>
        /// User
        /// </summary>
        private readonly User _user;

        /// <summary>
        /// Make marketplace requests authorized
        /// Unauthorized marketplace requests are possible, but with requests quota
        /// </summary>
        private readonly bool _marketplaceRequestsAuthorized;

        /// <summary>
        /// Enable trading
        /// </summary>
        private readonly bool _enableTrading = false;

        /// <summary>
        /// Wrong password
        /// </summary>
        private bool _wrongPassword;

        /// <summary>
        /// Authorization token
        /// </summary>
        private object _authorizationTokenLock = new object();
        private AuthorizationToken _authorizationToken;
        private AuthorizationToken AuthorizationToken
        {
            get
            {
                lock (_authorizationTokenLock)
                {
                    return _authorizationToken;
                }
            }
            set
            {
                lock (_authorizationTokenLock)
                {
                    _authorizationToken = value;
                }
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Zonky API
        /// </summary>
        private readonly Lazy<IZonkyApi> _zonkyApi;
        private IZonkyApi ZonkyApi
        {
            get { return _zonkyApi.Value; }
        }

        /// <summary>
        /// Zonky client
        /// </summary>
        public ZonkyClient()
        {
            _log = LogProvider.For<ZonkyClient>();
            _zonkyApi = new Lazy<IZonkyApi>(() => new ZonkyApi(new HttpClient()));

            _log.Debug($"Created Zonky client.");
        }

        /// <summary>
        /// Zonky client
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="enableTrading">Enable trading</param>
        public ZonkyClient(User user, bool enableTrading) : this(user, enableTrading, false) { }

        /// <summary>
        /// Zonky client
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="enableTrading">Enable trading</param>
        /// <param name="marketplaceRequestsAuthorized">Marketplace requests authorized</param>
        public ZonkyClient(User user, bool enableTrading, bool marketplaceRequestsAuthorized) : this()
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _enableTrading = enableTrading;
            _marketplaceRequestsAuthorized = marketplaceRequestsAuthorized;

            _log.Debug($"Zonky client with user {_user.Name} {(string.IsNullOrEmpty(_user.Password) ? "without password" : "with password")} {(_enableTrading ? "and enabled trading" : " and disabled trading")}.");
        }

        /// <summary>
        /// Zonky client
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="enableTrading">Enable trading</param>
        public ZonkyClient(string userName, string password, bool enableTrading) : this(new User(userName, password), enableTrading) { }

        /// <summary>
        /// Zonky client
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="enableTrading">Enable trading</param>
        /// <param name="marketplaceRequestsAuthorized">Marketplace requests authorized</param>
        public ZonkyClient(string userName, string password, bool enableTrading, bool marketplaceRequestsAuthorized) : this(new User(userName, password), enableTrading, marketplaceRequestsAuthorized) { }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task LoginAsync(CancellationToken ct = default)
        {
            CheckAuthorizationPrerequisites();

            try
            {
                _log.Debug($"Client requests login user {_user.Name}.");
                AuthorizationToken = await ZonkyApi.GetTokenExchangePasswordAsync(_user, ct).ConfigureAwait(false);
                _log.Debug($"User {_user.Name} logged in, new access token is {AuthorizationToken.AccessToken.ToString()}, refresh token {AuthorizationToken.RefreshToken.ToString()}.");
            }
            catch (MultiFactorAuthenticationRequiredException ex)
            {
                _log.Error(ex, $"Client requested for MFA with token {ex.MfaToken}");
                throw;
            }
            catch (BadLoginException ex)
            {
                _log.Error(ex, $"Client has wrong password for user {_user.Name}, another login tries are disabled.");
                _wrongPassword = false;
            }
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task RefreshTokenAsync(CancellationToken ct = default)
        {
            CheckAuthorizationPrerequisites();

            try
            {
                _log.Debug($"Client requests refresh token for user {_user.Name}, refresh token {AuthorizationToken.RefreshToken.ToString()}.");
                AuthorizationToken = await ZonkyApi.GetTokenExchangeRefreshTokenAsync(AuthorizationToken, ct).ConfigureAwait(false);
                _log.Debug($"Refresh token for user {_user.Name}, new access token is {AuthorizationToken.AccessToken.ToString()}, refresh token {AuthorizationToken.RefreshToken.ToString()}.");
            }
            catch (BadRefreshTokenException ex)
            {
                _log.Info(ex, $"Client retrying login as {_user.Name} after unsuccessful refresh token request.");
                await LoginAsync(ct).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Handle authorized request with return value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async Task<T> HandleAuthorizedRequestAsync<T>(Func<Task<T>> action, CancellationToken ct = default)
        {
            CheckAuthorizationPrerequisites();

            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (NotAuthorizedException ex)
            {
                _log.Debug(ex, $"Client request failed due to invalid authorization for user {_user.Name}.");
                await LoginAsync(ct).ConfigureAwait(false);
            }
            catch (BadAccessTokenException ex)
            {
                _log.Debug(ex, $"Client request failed due to invalid access token for user {_user.Name}.");
                await RefreshTokenAsync(ct).ConfigureAwait(false);
            }

            _log.Debug($"Client retrying request after refresh authorization token for user {_user.Name}.");
            return await action().ConfigureAwait(false);
        }

        /// <summary>
        /// Handle authorized request without return value
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async Task HandleAuthorizedRequestAsync(Func<Task> action, CancellationToken ct = default(CancellationToken))
        {
            CheckAuthorizationPrerequisites();

            try
            {
                await action().ConfigureAwait(false);
            }
            catch (NotAuthorizedException ex)
            {
                _log.Debug(ex, $"Client request failed due to invalid authorization for user {_user.Name}.");
                await LoginAsync(ct).ConfigureAwait(false);
            }
            catch (BadAccessTokenException ex)
            {
                _log.Debug(ex, $"Client request failed due to invalid access token for user {_user.Name}.");
                await RefreshTokenAsync(ct).ConfigureAwait(false);
            }

            _log.Debug($"Client retrying request after refresh authorization token for user {_user.Name}.");
            await action().ConfigureAwait(false);
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
        /// Split request for huge amount of data into many smaller requests 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="amount"></param>
        /// <param name="getAction"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async Task<List<T>> GetDataSplitRequestAsync<T>(int amount, Func<int, int, Task<IEnumerable<T>>> getAction, CancellationToken ct = default(CancellationToken))
        {
            var data = new List<T>();
            IEnumerable<T> dataPage;
            var page = 0;

            // Useful for very large portfolio, avoiding timeouts and server errors
            while ((dataPage = await getAction(page, amount).ConfigureAwait(false)).Any())
            {
                _log.Debug($"Get data page {page}, containing {dataPage.Count()} items.");

                ct.ThrowIfCancellationRequested();
                data.AddRange(dataPage);
                page++;

                // If his page is not full, skip check of next page
                if (dataPage.Count() < amount)
                {
                    break;
                }
            }

            return data;
        }

        /// <summary>
        /// Zonky client destructor
        /// </summary>
        ~ZonkyClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ZonkyApi.Dispose();
            }
        }
    }
}
