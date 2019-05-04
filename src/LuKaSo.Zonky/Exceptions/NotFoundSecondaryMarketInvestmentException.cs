using System;

namespace LuKaSo.Zonky.Exceptions
{
    public class NotFoundSecondaryMarketInvestmentException : Exception
    {
        public NotFoundSecondaryMarketInvestmentException(int offerId) : base($"Secondary market offer {offerId} was not found")
        { }
    }
}
