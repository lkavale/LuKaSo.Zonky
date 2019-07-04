using LuKaSo.Zonky.Logging;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Investor;
using LuKaSo.Zonky.Models.Overview;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient
    {
        /// <summary>
        /// Get investor wallet information
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Wallet> GetWalletAsync(CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetWalletAsync(_authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int page, int pageSize, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetNotificationsAsync(page, pageSize, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all user notifications
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken ct = default(CancellationToken))
        {
            _log.Debug($"Get all investor's notifications request.");

            var notifications = new List<Notification>();
            IEnumerable<Notification> notificationsPage;
            var page = 0;

            // Useful for very large portfolio, avoiding timeouts and server errors
            while ((notificationsPage = await GetNotificationsAsync(page, _maxPageSize, ct).ConfigureAwait(false)).Any())
            {
                _log.Debug($"Get all investor's notifications page {page}, contains {notificationsPage.Count()} notifications.");

                ct.ThrowIfCancellationRequested();
                notifications.AddRange(notificationsPage);
                page++;

                // If his page is not full, skip check of next page
                if (notificationsPage.Count() < _maxPageSize)
                {
                    break;
                }
            }

            // Distinct result for situation when new item is added when querying data 
            notifications = notifications.DistinctBy(n => n.Id).ToList();
            _log.Debug($"Get all investor's notifications {notifications.Count} fill {page} pages.");
            return notifications;
        }

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetWalletTransactionsAsync(_authorizationToken, filter, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get investor blocked amount
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetBlockedAmountAsync(_authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<InvestorOverview> GetInvestorOverviewAsync(CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetInvestorOverviewAsync(_authorizationToken, ct), ct).ConfigureAwait(false);
        }
    }
}
