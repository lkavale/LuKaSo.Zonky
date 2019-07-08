using LuKaSo.Zonky.Common;
using LuKaSo.Zonky.Models.Files;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient
    {
        /// <summary>
        /// Get loanbook data
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LoanbookItem>> GetLoanbookAsync(CancellationToken ct = default(CancellationToken))
        {
            var file = await ZonkyApi.GetLoanbookFileAddressAsync().ConfigureAwait(false);
            var processor = new SpreadsheetProcessor<LoanbookItem>();

            return await ZonkyApi.GetLoanbookAsync(file, processor, ct).ConfigureAwait(false);
        }
    }
}
