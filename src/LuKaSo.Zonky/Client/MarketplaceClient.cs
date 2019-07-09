using LuKaSo.Zonky.Logging;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Markets;
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
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            return await ZonkyApi.GetPrimaryMarketPlaceAsync(page, pageSize, filter, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all primary marketplace loans
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetAllPrimaryMarketPlaceAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            _log.Debug($"Get all primary market loans request.");

            // Get data
            var data = await GetDataSplitRequestAsync<Loan>(_maxPageSize, (page, pageSize) => GetPrimaryMarketPlaceAsync(page, pageSize, filter, ct), ct).ConfigureAwait(false);

            // Distinct result for situation when new item is added during querying data 
            data = data.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all primary market loans, total {data.Count} items.");
            return data;
        }

        /// <summary>
        /// Get all uncovered primary marketplace loans
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetAllUncoveredPrimaryMarketPlaceAsync(CancellationToken ct = default(CancellationToken))
        {
            var filter = new FilterOptions();
            filter.Add("nonReservedRemainingInvestment__gt", "0");

            return await GetAllPrimaryMarketPlaceAsync(filter, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all covered primary marketplace loans
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetAllCoveredPrimaryMarketPlaceAsync(CancellationToken ct = default(CancellationToken))
        {
            var filter = new FilterOptions();
            filter.Add("nonReservedRemainingInvestment__gt", "1");

            return await GetAllPrimaryMarketPlaceAsync(filter, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            return await ZonkyApi.GetSecondaryMarketplaceAsync(page, pageSize, filter, ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all secondary marketplace loans
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetAllSecondaryMarketplaceAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken))
        {
            _log.Debug($"Get all secondary market offers request.");

            // Get data
            var data = await GetDataSplitRequestAsync<SecondaryMarketOffer>(_maxPageSize, (page, pageSize) => GetSecondaryMarketplaceAsync(page, pageSize, filter, ct), ct).ConfigureAwait(false);

            // Distinct result for situation when new item is added during querying data 
            data = data.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all secondary market offers, total {data.Count} items.");
            return data;
        }
    }
}
