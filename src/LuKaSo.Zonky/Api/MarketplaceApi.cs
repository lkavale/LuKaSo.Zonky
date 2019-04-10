using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Loans;
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
        /// Get primary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, CancellationToken ct)
        {
            var url = _baseUrl.Append("/loans/marketplace");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
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
                            return JsonConvert.DeserializeObject<IEnumerable<Loan>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
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
        /// Get secondary marketplace loans
        /// </summary>
        /// <param name="page">Page number, started from 0</param>
        /// <param name="pageSize"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, CancellationToken ct)
        {
            var url = _baseUrl.Append("/smp/investments");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
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
                            return JsonConvert.DeserializeObject<IEnumerable<SecondaryMarketOffer>>(responseData, _settings.Value);
                        }
                        catch (Exception ex)
                        {
                            throw new BadResponceException(response, ex);
                        }
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
    }
}
