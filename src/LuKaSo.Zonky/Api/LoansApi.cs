using LuKaSo.Zonky.Api.Exceptions;
using LuKaSo.Zonky.Api.Extesions;
using LuKaSo.Zonky.Api.Models.Loans;
using LuKaSo.Zonky.Api.Models.Login;
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
        /// Get loan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Loan> GetLoanAsync(int id, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/loans/{id}");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

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
                            return JsonConvert.DeserializeObject<Loan>(responseData, _settings.Value);
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
        /// Get list of investors and amounts engaged in the loan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LoanInvestment>> GetLoanInvestmentsAsync(int id, AuthorizationToken authorizationToken, CancellationToken ct)
        {
            var url = _baseUrl.Append($"/loans/{id}/investments");

            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = url;
                request.Method = new HttpMethod("GET");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken.AccessToken.ToString());
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

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
                            return JsonConvert.DeserializeObject<IEnumerable<LoanInvestment>>(responseData, _settings.Value);
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
    }
}
