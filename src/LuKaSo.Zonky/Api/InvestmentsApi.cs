using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Investments;
using LuKaSo.Zonky.Models.Login;
using LuKaSo.Zonky.Models.Markets;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi
    {
        /// <summary>
        /// Get investor list of investments
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/users/me/investments", authorizationToken, page, pageSize))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<Investment>>(response, true);
            }
        }

        /// <summary>
        /// Get events list for investment
        /// </summary>
        /// <param name="loanId">Loan Id</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/collections/loans/{loanId}/investor-events", authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                return await ExtractResponceOkErrorDataAsync<IEnumerable<InvestmentEvent>>(response, true);
            }
        }

        /// <summary>
        /// Create primary market investment
        /// </summary>
        /// <param name="investment">Primary market investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest("/marketplace/investment", HttpMethod.Post, authorizationToken).AddJsonContent(investment, Settings))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                await _resolverFactory.Create(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK, (message) => { })
                    .ConfigureStatusResponce(HttpStatusCode.BadRequest, (message) => throw new PrimaryMarketInvestmentException(investment, message))
                    .ConfigureStatusResponce(HttpStatusCode.Forbidden, (message) => throw new CaptchaValidationException(message))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
            }
        }

        /// <summary>
        /// Increase primary market investment
        /// </summary>
        /// <param name="investmentId">Investment Id</param>
        /// <param name="increaseInvestment">Primary market increase investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/marketplace/investment/{investmentId}", new HttpMethod("PATCH"), authorizationToken).AddJsonContent(increaseInvestment, Settings))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                await _resolverFactory.Create(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK, (message) => { })
                    .ConfigureStatusResponce(HttpStatusCode.BadRequest, (message) => throw new PrimaryMarketInvestmentException(investmentId, increaseInvestment, message))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
            }
        }

        /// <summary>
        /// Buy secondary market investment offer
        /// </summary>
        /// <param name="offerId">Id of investment offer</param>
        /// <param name="secondaryMarketInvestment">Secondary market investment</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/smp/investments/{offerId}/shares", HttpMethod.Post, authorizationToken).AddJsonContent(secondaryMarketInvestment, Settings))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                await _resolverFactory.Create(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK, (message) => { })
                    .ConfigureStatusResponce(HttpStatusCode.NoContent, (message) => { })
                    .ConfigureStatusResponce<SecondaryMarketBuyError>(HttpStatusCode.BadRequest, (error, message) => throw new BuySecondaryMarketInvestmentException(offerId, secondaryMarketInvestment, error))
                    .ConfigureStatusResponce(HttpStatusCode.NotFound, (message) => throw new NotFoundSecondaryMarketInvestmentException(offerId))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
            }
        }

        /// <summary>
        /// Offer investment on secondary market to sell
        /// </summary>
        /// <param name="secondaryMarketOfferSell">Secondary market offer to sell</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/users/me/traded-investments", HttpMethod.Post, authorizationToken).AddJsonContent(secondaryMarketOfferSell, Settings))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                await _resolverFactory.Create(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK, (message) => { })
                    .ConfigureStatusResponce(HttpStatusCode.NoContent, (message) => { })
                    .ConfigureStatusResponce<SecondaryMarketOfferSellError>(HttpStatusCode.BadRequest, (error, message) => throw new OfferInvestmentSecondaryMarketException(secondaryMarketOfferSell, error))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
            }
        }

        /// <summary>
        /// Cancel offer to sell on secondary market
        /// </summary>
        /// <param name="offerId">Secondary market offer Id</param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, AuthorizationToken authorizationToken, CancellationToken ct = default)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = PrepareAuthorizedRequest($"/users/me/traded-investments/{offerId}", HttpMethod.Delete, authorizationToken))
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct).ConfigureAwait(false))
            {
                await _resolverFactory.Create(Settings, true)
                    .ConfigureStatusResponce(HttpStatusCode.OK, (message) => { })
                    .ConfigureStatusResponce(HttpStatusCode.NoContent, (message) => { })
                    .ConfigureStatusResponce<SecondaryMarketOfferCancelError>(HttpStatusCode.Gone, (error, message) => throw new CancelSecondartMarketOfferException(offerId, error))
                    .ConfigureDefaultResponce((message) => throw new ServerErrorException(message))
                    .ExtractDataAsync(response);
            }
        }
    }
}
