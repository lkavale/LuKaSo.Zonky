using HtmlAgilityPack;
using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Models.Files;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Api
{
    public partial class ZonkyApi
    {
        /// <summary>
        /// Get loanbook data
        /// </summary>
        /// <param name="loanbookFileUrl"></param>
        /// <param name="processor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LoanbookItem>> GetLoanbookAsync(Uri loanbookFileUrl, SpreadsheetProcessor<LoanbookItem>  processor, CancellationToken ct = default(CancellationToken))
        {
            using (var loanbookFile = await _httpClient.GetStreamAsync(loanbookFileUrl).ConfigureAwait(false))
            using (var memoryStream = new MemoryStream())
            {
                await loanbookFile.CopyToAsync(memoryStream).ConfigureAwait(false);
                memoryStream.Position = 0;

                var workbook = new XSSFWorkbook(memoryStream);
                return processor.ProcessWorkbook(workbook);
            }
        }

        /// <summary>
        /// Get URL of loanbook file
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Uri> GetLoanbookFileAddressAsync(CancellationToken ct = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri("https://zonky.cz/risk/");
                request.Method = new HttpMethod("GET");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var document = new HtmlDocument();
                        document.Load(responseStream);

                        try
                        {
                            var link = document.DocumentNode
                                .SelectNodes(".//a[@data-action='url']")
                                .Single(a => a.GetAttributeValue("href", "").Contains("zonky.cz/page-assets/images/risk/loanbook/"));

                            var loanbookLink = link.GetAttributeValue("href", "")
                                .Replace("clkn/https", "https:/");

                            return new Uri(loanbookLink);
                        }
                        catch (Exception ex)
                        {
                            throw new LinkNotFoundException(response, ex);
                        }
                    }

                    throw new ServerErrorException(response);
                }
            }
        }
    }
}
