using LuKaSo.Zonky.Models.Markets;
using System;

namespace LuKaSo.Zonky.Exceptions
{
    public class CancelSecondartMarketOfferException : Exception
    {
        public CancelSecondartMarketOfferException(int investmentId, SecondaryMarketOfferCancelError secondaryMarketOfferCancelError) : base($"Cancel secondary market investment offer id {investmentId} failed with {secondaryMarketOfferCancelError.ToString()}")
        { }
    }
}
