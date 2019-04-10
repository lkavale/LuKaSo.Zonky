using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Investments;
using LuKaSo.Zonky.Api.Models.Login;
using LuKaSo.Zonky.Api.Models.Markets;
using Newtonsoft.Json;
using System;
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
        /// <param name="pageSize"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Investment>> GetInvestmentsAsync(int page, int pageSize, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append("/users/me/investments");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
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
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<Investment>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
        /// <param name="loanId"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InvestmentEvent>> GetInvestmentEventsAsync(int loanId, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/collections/loans/{loanId}/investor-events");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        try
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<InvestmentEvent>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else
                    {
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
        /// <param name="investment"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task CreatePrimaryMarketInvestmentAsync(PrimaryMarketInvestment investment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append("/marketplace/investment");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("POST");
                request.Content = new StringContent(JsonConvert.SerializeObject(investment, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        //TODO impelement return codes
                        //throw new SwaggerException("The input is invalid.\n\nPossible error codes are:\n\n* ```CAPTCHA_REQUIRED``` - Captcha verification is required\n\n* ```insufficientBalance``` - The user cannot invest because of a low wallet balance.\n\n* ```cancelled``` - The loan has been canceled\n\n* ```withdrawn``` - The loan has been withdrawn by the borrower\n\n* ```reservedInvestmentOnly``` - The user cannot invest without a reservation. The whole remaining investment is reserved for other users. See `remainingInvestment` and `reservedAmount`.\n\n* ```overInvestment``` - The amount for investment is too high. See `remainingInvestment`.\n\n* ```multipleInvestment``` - The user has already invested to this loan. Try increasing the investment instead.\n\n* ```alreadyCovered``` - The whole loan amount has been already covered. See `remainingInvestment`.", (int)response_.StatusCode, responseData_, headers_, null);
                    }
                    else if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        throw new CaptchaValidationException();
                    }
                    else
                    {
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
        /// <param name="investment"></param>
        /// <param name="increaseInvestment"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task IncreasePrimaryMarketInvestmentAsync(int investment, IncreasePrimaryMarketInvestment increaseInvestment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/marketplace/investment/{investment}");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("PATCH");
                request.Content = new StringContent(JsonConvert.SerializeObject(increaseInvestment, _settings.Value));
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());

                var response = await _httpClient
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);

                try
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new NotAuthorizedException();
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        //TODO impelement return codes
                        //throw new SwaggerException("The input is invalid.\n\nPossible error codes are:\n\n* ```insufficientBalance``` - The user cannot invest because of a low wallet balance.\n\n* ```cancelled``` - The loan has been canceled\n\n* ```withdrawn``` - The loan has been withdrawn by the borrower\n\n* ```reservedInvestmentOnly``` - The user cannot invest without a reservation. The whole remaining investment is reserved for other users. See `remainingInvestment` and `reservedAmount`.\n\n* ```overInvestment``` - The amount for investment is too high. See `remainingInvestment`.\n\n* ```increaseInvestmentNotExist``` - The user has not invested to the loan yet. Try investing first.\n\n* ```alreadyCovered``` - The whole loan amount has been already covered. See `remainingInvestment`.", (int)response_.StatusCode, responseData_, headers_, null);
                    }
                    else
                    {
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
        /// Buy secondary market investment
        /// </summary>
        /// <param name="investment">Id of an investment offer</param>
        /// <param name="secondaryMarketInvestment"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task BuySecondaryMarketInvestmentAsync(int investmentId, SecondaryMarketInvestment secondaryMarketInvestment, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/smp/investments/{investmentId}/shares");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
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
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            try
                            {
                                JsonConvert.DeserializeObject<SecondaryMarketBuyError>(responseData, _settings.Value);
                            }
                            catch (Exception ex)
                            {
                                throw new BadResponceException(response, ex);
                            }
                            throw new Exception();
                        case HttpStatusCode.NotFound:
                        case HttpStatusCode.RequestTimeout:
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
        /// <param name="investmentSecondaryMarketOffer"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task OfferInvestmentOnSecondaryMarketAsync(SecondaryMarketOfferSell secondaryMarketOfferSell, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/users/me/traded-investments");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
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
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.BadRequest:
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            try
                            {
                                JsonConvert.DeserializeObject<SecondaryMarketOfferSellError>(responseData, _settings.Value);
                            }
                            catch (Exception ex)
                            {
                                throw new BadResponceException(response, ex);
                            }
                            throw new Exception();
                        case HttpStatusCode.RequestTimeout:
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
        /// <param name="investmentId"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task OfferInvestmentOnSecondaryMarketAsync(int investmentId, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/users/me/traded-investments/{investmentId}");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
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
                        case HttpStatusCode.Created:
                            return;
                        case HttpStatusCode.Unauthorized:
                            throw new NotAuthorizedException();
                        case HttpStatusCode.Gone:
                            var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            try
                            {
                                JsonConvert.DeserializeObject<SecondaryMarketOfferCancelError>(responseData, _settings.Value);
                            }
                            catch (Exception ex)
                            {
                                throw new BadResponceException(response, ex);
                            }
                            throw new Exception();
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
