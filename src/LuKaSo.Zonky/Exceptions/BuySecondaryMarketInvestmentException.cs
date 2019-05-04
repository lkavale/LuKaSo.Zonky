using LuKaSo.Zonky.Models.Markets;
using System;

namespace LuKaSo.Zonky.Exceptions
{
    public class BuySecondaryMarketInvestmentException : Exception
    {
        public BuySecondaryMarketInvestmentException(int investmentId, SecondaryMarketInvestment secondaryMarketInvestment, SecondaryMarketBuyError secondaryMarketBuyError) : base($"Buy secondary market investment id {investmentId} request {secondaryMarketInvestment.ToString()} failed with {secondaryMarketBuyError.ToString()}")
        { }
    }
}
