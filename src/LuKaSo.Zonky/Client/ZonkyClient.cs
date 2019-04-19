﻿using LuKaSo.Zonky.Api;
using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient : IZonkyClient, IDisposable
    {
        /// <summary>
        /// Max paging size, requests for huge item amount (eg. primary market, large investment portfolio > 1000 participations, etc.)
        /// failing on timeouts very often, so it is better to split the request to many 
        /// </summary>
        private const int _maxPageSize = 500;

        /// <summary>
        /// User
        /// </summary>
        private readonly User _user;

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
        private AuthorizationToken _authorizationToken;

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
        public ZonkyClient(User user, bool enableTrading) : this()
        {
            _user = user ?? throw new ArgumentNullException();
            _enableTrading = enableTrading;

            _log.Debug($"Zonky client with user {_user.Name} {(_enableTrading ? "and enabled trading" : " and disabled trading")}.");
        }

        /// <summary>
        /// Zonky client
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <param name="enableTrading">Enable trading</param>
        public ZonkyClient(string userName, string password, bool enableTrading) : this()
        {
            _user = new User(userName, password);
            _enableTrading = enableTrading;

            _log.Debug($"Zonky client with user {_user.Name} {(_enableTrading ? "and enabled trading" : " and disabled trading")}.");
        }

        /// <summary>
        /// Handle authorized request with return value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async Task<T> HandleAuthorizedRequestAsync<T>(Func<Task<T>> action, CancellationToken ct = default(CancellationToken))
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
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async Task HandleAuthorizedRequestAsync<T>(Func<Task> action, CancellationToken ct = default(CancellationToken))
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
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ZonkyApi.Dispose();
            }
        }
    }
}