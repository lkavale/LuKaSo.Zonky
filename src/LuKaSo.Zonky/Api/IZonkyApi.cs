using LuKaSo.Zonky.Api.Models.Investments;
using LuKaSo.Zonky.Api.Models.Loans;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Api.Models.Markets;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public interface IZonkyApi
    {
        /// <summary>
        /// Get access token exchange with password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AuthorizationToken> GetTokenExchangePasswordAsync(User user, CancellationToken ct);

        /// <summary>
        /// Get access token exchange with refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<AuthorizationToken> GetTokenExchangeRefreshTokenAsync(AuthorizationToken token, CancellationToken ct);

        /// <summary>
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, CancellationToken ct);

        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<Loan> GetLoanAsync(int id, CancellationToken ct);

        /// <summary>
        /// Get list of investors and amounts engaged in the loan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<LoanInvestment>> GetLoanInvestmentsAsync(int id, AuthorizationToken authorizationToken, CancellationToken ct);

        /// <summary>
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, CancellationToken ct);

        /// <summary>
        /// Get investor list of investments
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct);
    }
}
