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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<IEnumerable<Investment>>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await ExtractDataAsync<IEnumerable<InvestmentEvent>>(response).ConfigureAwait(false);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return;
                    case HttpStatusCode.BadRequest:
                        throw new PrimaryMarketInvestmentException(investment, response);
                    case HttpStatusCode.Forbidden:
                        throw new CaptchaValidationException(response);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return;
                    case HttpStatusCode.BadRequest:
                        throw new PrimaryMarketInvestmentException(investmentId, increaseInvestment, response);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return;
                    case HttpStatusCode.BadRequest:
                        var secondaryMarketBuyError = await ExtractDataAsync<SecondaryMarketBuyError>(response).ConfigureAwait(false);
                        throw new BuySecondaryMarketInvestmentException(offerId, secondaryMarketInvestment, secondaryMarketBuyError);
                    case HttpStatusCode.NotFound:
                        throw new NotFoundSecondaryMarketInvestmentException(offerId);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                CheckAuthorizedResponce(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return;
                    case HttpStatusCode.BadRequest:
                        var secondaryMarketOfferSellError = await ExtractDataAsync<SecondaryMarketOfferSellError>(response).ConfigureAwait(false);
                        throw new OfferInvestmentSecondaryMarketException(secondaryMarketOfferSell, secondaryMarketOfferSellError);
                    default:
                        throw new ServerErrorException(response);
                }
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
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.NoContent:
                        return;
                    case HttpStatusCode.Gone:
                        var secondaryMarketOfferCancelError = await ExtractDataAsync<SecondaryMarketOfferCancelError>(response).ConfigureAwait(false);
                        throw new CancelSecondartMarketOfferException(offerId, secondaryMarketOfferCancelError);
                    default:
                        throw new ServerErrorException(response);
                }
            }
        }
    }
}
