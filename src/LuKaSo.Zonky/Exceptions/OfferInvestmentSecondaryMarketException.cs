using LuKaSo.Zonky.Models.Markets;
using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class OfferInvestmentSecondaryMarketException : Exception
    {
        public OfferInvestmentSecondaryMarketException(SecondaryMarketOfferSell secondaryMarketOfferSell, SecondaryMarketOfferSellError secondaryMarketOfferSellError) : base($"Offer investment on secondary market {secondaryMarketOfferSell.ToString()} failed with {secondaryMarketOfferSellError.ToString()}")
        { }

        protected OfferInvestmentSecondaryMarketException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
