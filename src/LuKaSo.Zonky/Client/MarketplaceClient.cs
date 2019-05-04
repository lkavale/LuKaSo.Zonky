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

            var loans = new List<Loan>();
            IEnumerable<Loan> loansPage;
            var page = 0;

            // Useful for very large data amount, avoiding timeouts and server errors
            while ((loansPage = await GetPrimaryMarketPlaceAsync(page, _maxPageSize, filter, ct).ConfigureAwait(false)).Any())
            {
                _log.Debug($"Get all primary market loans page {page}, contains {loansPage.Count()} loans.");

                ct.ThrowIfCancellationRequested();
                loans.AddRange(loansPage);
                page++;

                // If his page is not full, skip check of next page
                if (loansPage.Count() < _maxPageSize)
                {
                    break;
                }
            }

            // Distinct result for situation when new item is added when querying data 
            loans = loans.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all primary market loans {loans.Count} fill {page} pages.");
            return loans;
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

            var offers = new List<SecondaryMarketOffer>();
            IEnumerable<SecondaryMarketOffer> offersPage;
            var page = 0;

            // Useful for very large data amount, avoiding timeouts and server errors
            while ((offersPage = await GetSecondaryMarketplaceAsync(page, _maxPageSize, filter, ct).ConfigureAwait(false)).Any())
            {
                _log.Debug($"Get all secondary market offers page {page}, contains {offersPage.Count()} offers.");

                ct.ThrowIfCancellationRequested();
                offers.AddRange(offersPage);
                page++;

                // If his page is not full, skip check of next page
                if (offersPage.Count() < _maxPageSize)
                {
                    break;
                }
            }

            // Distinct result for situation when new item is added when querying data 
            offers = offers.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all secondary market offers {offers.Count} fill {page} pages.");
            return offers;
        }
    }
}
