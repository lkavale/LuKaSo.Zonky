using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Files;
using LuKaSo.Zonky.Models.Investments;
using LuKaSo.Zonky.Models.Investor;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Markets;
using LuKaSo.Zonky.Models.Overview;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public interface IZonkyClient : IDisposable
    {
        /// <summary>
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all primary marketplace loans
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Loan>> GetAllPrimaryMarketPlaceAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all uncovered primary marketplace loans
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Loan>> GetAllUncoveredPrimaryMarketPlaceAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all covered primary marketplace loans
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Loan>> GetAllCoveredPrimaryMarketPlaceAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all secondary marketplace loans
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<SecondaryMarketOffer>> GetAllSecondaryMarketplaceAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Loan> GetLoanAsync(int loanId, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get list of investors and amounts engaged in the loan
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<LoanInvestment>> GetLoanInvestmentsAsync(int loanId, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor wallet information
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Wallet> GetWalletAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotificationsAsync(int page, int pageSize, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all user notifications
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetAllNotificationsAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor wallet transactions
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor blocked amount
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor's investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get all investor's investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Investment>> GetAllInvestmentsAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get events list for investment
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Create primary market investment
        /// </summary>
        /// <param name="investment">Primary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Increase primary market investment
        /// </summary>
        /// <param name="investmentId">Investment Id</param>
        /// <param name="increaseInvestment">Primary market increase investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Buy secondary market investment offer
        /// </summary>
        /// <param name="offerId">Id of investment offer</param>
        /// <param name="secondaryMarketInvestment">Secondary market investment</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Offer investment on secondary market to sell
        /// </summary>
        /// <param name="secondaryMarketOfferSell">Secondary market offer to sell</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Cancel offer to sell on secondary market
        /// </summary>
        /// <param name="offerId">Secondary market offer Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get loanbook data
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<LoanbookItem>> GetLoanbookAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<InvestorOverview> GetInvestorOverviewAsync(CancellationToken ct = default(CancellationToken));
    }
}
