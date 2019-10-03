using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Extesions;
using LuKaSo.Zonky.Models.Loans;
using LuKaSo.Zonky.Models.Login;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi
    {
        /// <summary>
        /// Get loan
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Loan> GetLoanAsync(int loanId, CancellationToken ct = default)
        {
            using (var request = new ZonkyHttpRequestMessage(HttpMethod.Get, _baseUrl.Append($"/loans/{loanId}")))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<Loan>(response);
            }
        }

        /// <summary>
        /// Get list of investors and amounts engaged in the loan
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LoanInvestment>> GetLoanInvestmentsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/loans/{loanId}/investments", authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<LoanInvestment>>(response, true);
            }
        }
    }
}
