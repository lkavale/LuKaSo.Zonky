using LuKaSo.Zonky.Exceptions;
using LuKaSo.Zonky.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Client
{
    public partial class ZonkyClient
    {
        /// <summary>
        /// Check trading prerequisites
        /// </summary>
        private void CheckTradingPrerequisites()
        {
            if (!_enableTrading)
            {
                _log.Debug($"Zonky client could not make trading request due to disabled trading.");
                throw new TradingNotAllowedException();
            }
        }


    }
}
