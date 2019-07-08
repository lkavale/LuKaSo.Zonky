using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Models;
using LuKaSo.Zonky.Models.Files;
using LuKaSo.Zonky.Models.Investments;
using LuKaSo.Zonky.Models.Investor;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Markets;
using LuKaSo.Zonky.Models.Overview;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public interface IZonkyApi : IDisposable
    {
        /// <summary>
        /// Get access token exchange with password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get access token exchange with refresh token
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AuthorizationToken> GetTokenExchangeRefreshTokenAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

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
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="filter">Filter options</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

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
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<LoanInvestment>> GetLoanInvestmentsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor wallet information
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Wallet> GetWalletAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get user notifications
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotificationsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor's wallet transactions
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Number of messages</param>
        /// <param name="filter">Filter options</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<WalletTransaction>> GetWalletTransactionsAsync(int page, int pageSize, AuthorizationToken authorizationToken, FilterOptions filter = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor blocked amount
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<BlockedAmount>> GetBlockedAmountAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor list of investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get events list for investment
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Create primary market investment
        /// </summary>
        /// <param name="investment">Primary market investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Increase primary market investment
        /// </summary>
        /// <param name="investmentId">Investment Id</param>
        /// <param name="increaseInvestment">Primary market increase investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Buy secondary market investment offer
        /// </summary>
        /// <param name="offerId">Id of investment offer</param>
        /// <param name="secondaryMarketInvestment">Secondary market investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Offer investment on secondary market to sell
        /// </summary>
        /// <param name="secondaryMarketOfferSell">Secondary market offer to sell</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Cancel offer to sell on secondary market
        /// </summary>
        /// <param name="offerId">Secondary market offer Id</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get loanbook data
        /// </summary>
        /// <param name="loanbookFileUrl"></param>
        /// <param name="processor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<LoanbookItem>> GetLoanbookAsync(Uri loanbookFileUrl, SpreadsheetProcessor<LoanbookItem> processor, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get URL of loanbook file
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Uri> GetLoanbookFileAddressAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<InvestorOverview> GetInvestorOverviewAsync(AuthorizationToken authorizationToken, CancellationToken ct = default(CancellationToken));
    }
}
