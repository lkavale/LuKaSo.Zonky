using LuKaSo.Zonky.Api.Models.Markets;
using System;

namespace LuKaSo.Zonky.Api.Exceptions
{
    public class OfferInvestmentSecondaryMarketException : Exception
    {
        public OfferInvestmentSecondaryMarketException(SecondaryMarketOfferSell secondaryMarketOfferSell, SecondaryMarketOfferSellError secondaryMarketOfferSellError) : base($"Offer investment on secondary market {secondaryMarketOfferSell.ToString()} failed with {secondaryMarketOfferSellError.ToString()}")
        { }
    }
}
