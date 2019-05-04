using LuKaSo.Zonky.Api.Models.Investments;
using LuKaSo.Zonky.Api.Models.Markets;
using LuKaSo.Zonky.Logging;
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
        /// Get investor's investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetInvestmentsAsync(page, pageSize, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all investor's investments
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetAllInvestmentsAsync(CancellationToken ct = default(CancellationToken))
        {
            _log.Debug($"Get all investor's participations request.");

            var participations = new List<Investment>();
            IEnumerable<Investment> participationsPage;
            var page = 0;

            // Useful for very large portfolio, avoiding timeouts and server errors
            while ((participationsPage = await GetInvestmentsAsync(page, _maxPageSize, ct).ConfigureAwait(false)).Any())
            {
                _log.Debug($"Get all investor's participations page {page}, contains {participationsPage.Count()} participations.");

                ct.ThrowIfCancellationRequested();
                participations.AddRange(participationsPage);
                page++;

                // If his page is not full, skip check of next page
                if (participationsPage.Count() < _maxPageSize)
                {
                    break;
                }
            }

            // Distinct result for situation when new item is added when querying data 
            participations = participations.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all investor's participations {participations.Count} fill {page} pages.");
            return participations;
        }

        /// <summary>
        /// Get events list for investment
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, CancellationToken ct = default(CancellationToken))
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetInvestmentEventsAsync(loanId, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Create primary market investment
        /// </summary>
        /// <param name="investment">Primary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, CancellationToken ct = default(CancellationToken))
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.CreatePrimaryMarketInvestmentAsync(investment, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Increase primary market investment
        /// </summary>
        /// <param name="investmentId">Investment Id</param>
        /// <param name="increaseInvestment">Primary market increase investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, CancellationToken ct = default(CancellationToken))
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.IncreasePrimaryMarketInvestmentAsync(investmentId, increaseInvestment, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Buy secondary market investment offer
        /// </summary>
        /// <param name="offerId">Id of investment offer</param>
        /// <param name="secondaryMarketInvestment">Secondary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, CancellationToken ct = default(CancellationToken))
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.BuySecondaryMarketInvestmentAsync(offerId, secondaryMarketInvestment, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Offer investment on secondary market to sell
        /// </summary>
        /// <param name="secondaryMarketOfferSell">Secondary market offer to sell</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, CancellationToken ct = default(CancellationToken))
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.OfferInvestmentOnSecondaryMarketAsync(secondaryMarketOfferSell, _authorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel offer to sell on secondary market
        /// </summary>
        /// <param name="offerId">Secondary market offer Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, CancellationToken ct = default(CancellationToken))
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.CancelOfferInvestmentOnSecondaryMarketAsync(offerId, _authorizationToken, ct), ct).ConfigureAwait(false);
        }
    }
}
