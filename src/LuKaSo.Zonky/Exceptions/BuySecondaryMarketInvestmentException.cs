using LuKaSo.Zonky.Models.Markets;
using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class BuySecondaryMarketInvestmentException : Exception
    {
        public BuySecondaryMarketInvestmentException(int investmentId, SecondaryMarketInvestment secondaryMarketInvestment, SecondaryMarketBuyError secondaryMarketBuyError) : base($"Buy secondary market investment id {investmentId} request {secondaryMarketInvestment.ToString()} failed with {secondaryMarketBuyError.ToString()}")
        { }

        protected BuySecondaryMarketInvestmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
