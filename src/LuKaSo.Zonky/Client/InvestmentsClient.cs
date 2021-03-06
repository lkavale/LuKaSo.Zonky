﻿using LuKaSo.Zonky.Logging;
using LuKaSo.Zonky.Models.Investments;
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
        /// Get investor's investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetInvestmentsAsync(page, pageSize, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all investor's investments
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetAllInvestmentsAsync(CancellationToken ct = default)
        {
            _log.Debug($"Get all investor's participations request.");

            // Get data
            var data = await GetDataSplitRequestAsync(_maxPageSize, (page, pageSize) => GetInvestmentsAsync(page, pageSize, ct), ct).ConfigureAwait(false);

            // Distinct result for situation when new item is added during querying data 
            data = data.DistinctBy(l => l.Id).ToList();
            _log.Debug($"Get all investor's participations, total {data.Count} items.");
            return data;
        }

        /// <summary>
        /// Get events list for investment
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, CancellationToken ct = default)
        {
            return await HandleAuthorizedRequestAsync(() => ZonkyApi.GetInvestmentEventsAsync(loanId, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Create primary market investment
        /// </summary>
        /// <param name="investment">Primary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, CancellationToken ct = default)
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.CreatePrimaryMarketInvestmentAsync(investment, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Increase primary market investment
        /// </summary>
        /// <param name="investmentId">Investment Id</param>
        /// <param name="increaseInvestment">Primary market increase investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, CancellationToken ct = default)
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.IncreasePrimaryMarketInvestmentAsync(investmentId, increaseInvestment, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Buy secondary market investment offer
        /// </summary>
        /// <param name="offerId">Id of investment offer</param>
        /// <param name="secondaryMarketInvestment">Secondary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, CancellationToken ct = default)
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.BuySecondaryMarketInvestmentAsync(offerId, secondaryMarketInvestment, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Offer investment on secondary market to sell
        /// </summary>
        /// <param name="secondaryMarketOfferSell">Secondary market offer to sell</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, CancellationToken ct = default)
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.OfferInvestmentOnSecondaryMarketAsync(secondaryMarketOfferSell, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel offer to sell on secondary market
        /// </summary>
        /// <param name="offerId">Secondary market offer Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, CancellationToken ct = default)
        {
            CheckTradingPrerequisites();

            await HandleAuthorizedRequestAsync(() => ZonkyApi.CancelOfferInvestmentOnSecondaryMarketAsync(offerId, AuthorizationToken, ct), ct).ConfigureAwait(false);
        }
    }
}
