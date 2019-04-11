using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Investments;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Api.Models.Markets;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/users/me/investments");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());
                request.Headers.Add("x-page", page.ToString());
                request.Headers.Add("x-size", pageSize.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<Investment>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append($"/collections/loans/{loanId}/investor-events");
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return await ExtractDataAsync<IEnumerable<InvestmentEvent>>(response).ConfigureAwait(false);
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/marketplace/investment");
                request.Method = new HttpMethod("POST");
                request.Content = new StringContent(JsonConvert.SerializeObject(investment, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw new PrimaryMarketInvestmentException(investment, response);
                        case HttpStatusCode.Forbidden:
                            throw new CaptchaValidationException(response);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task IncreasePrimaryMarketInvestmentAsync(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append($"/marketplace/investment/{investmentId}");
                request.Method = new HttpMethod("PATCH");
                request.Content = new StringContent(JsonConvert.SerializeObject(increaseInvestment, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            throw new PrimaryMarketInvestmentException(investmentId, increaseInvestment, response);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task BuySecondaryMarketInvestmentAsync(int offerId, SecondaryMarketInvestment secondaryMarketInvestment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append($"/smp/investments/{offerId}/shares");
                request.Method = new HttpMethod("POST");
                request.Content = new StringContent(JsonConvert.SerializeObject(secondaryMarketInvestment, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            var secondaryMarketBuyError = await ExtractDataAsync<SecondaryMarketBuyError>(response).ConfigureAwait(false);
                            throw new BuySecondaryMarketInvestmentException(offerId, secondaryMarketInvestment, secondaryMarketBuyError);
                        case HttpStatusCode.NotFound:
                            throw new NotFoundSecondaryMarketInvestmentException(offerId);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append($"/users/me/traded-investments");
                request.Method = new HttpMethod("POST");
                request.Content = new StringContent(JsonConvert.SerializeObject(secondaryMarketOfferSell, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            var secondaryMarketOfferSellError = await ExtractDataAsync<SecondaryMarketOfferSellError>(response).ConfigureAwait(false);
                            throw new OfferInvestmentSecondaryMarketException(secondaryMarketOfferSell, secondaryMarketOfferSellError);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
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
        public async Task CancelOfferInvestmentOnSecondaryMarketAsync(int offerId, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            CheckAuthorizationToken(authorizationToken);

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append($"/users/me/traded-investments/{offerId}");
                request.Method = new HttpMethod("DELETE");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.Gone:
                            var secondaryMarketOfferCancelError = await ExtractDataAsync<SecondaryMarketOfferCancelError>(response).ConfigureAwait(false);
                            throw new CancelSecondartMarketOfferException(offerId, secondaryMarketOfferCancelError);
                        default:
                            throw new ServerErrorException(response);
                    }
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
        }
    }
}
