using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class NotFoundSecondaryMarketInvestmentException : Exception
    {
        public NotFoundSecondaryMarketInvestmentException(int offerId) : base($"Secondary market offer {offerId} was not found")
        { }

        protected NotFoundSecondaryMarketInvestmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
