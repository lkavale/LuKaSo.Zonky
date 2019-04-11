using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Loans;
using LuKaSo.Zonky.Api.Models.Markets;
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
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Loan>> GetPrimaryMarketPlaceAsync(int page, int pageSize, CancellationToken ct)
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/loans/marketplace");
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
                        return await ExtractDataAsync<IEnumerable<Loan>>(response).ConfigureAwait(false);
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
        /// <param name="pageSize">Items per page</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SecondaryMarketOffer>> GetSecondaryMarketplaceAsync(int page, int pageSize, CancellationToken ct)
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("/smp/investments");
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
                        return await ExtractDataAsync<IEnumerable<SecondaryMarketOffer>>(response).ConfigureAwait(false);
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
