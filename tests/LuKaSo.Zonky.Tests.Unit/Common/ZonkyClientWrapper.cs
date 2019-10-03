using LuKaSo.Zonky.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Tests.Unit.Common
{
    public class ZonkyClientWrapper : ZonkyClient
    {
        public async Task<List<T>> GetDataSplitRequestWrappedAsync<T>(int amount, Func<int, int, Task<IEnumerable<T>>> getAction, CancellationToken ct = default)
        {
            return await GetDataSplitRequestAsync<T>(amount, getAction, ct).ConfigureAwait(false);
        }
    }
}
