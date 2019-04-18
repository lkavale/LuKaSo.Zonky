using LuKaSo.Zonky.Api.Models;
using LuKaSo.Zonky.Api.Models.Investor;
using System.Collections.Generic;
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
        /// <param name="size">Number of messages</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int size, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetNotificationsAsync(size, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(FilterOptions filter, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetWalletTransactionsAsync(filter, _authorizationToken, ct), ct).ConfigureAwait(false);
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
    }
}
