using LuKaSo.Zonky.Models.Markets;
using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class CancelSecondartMarketOfferException : Exception
    {
        public CancelSecondartMarketOfferException(int investmentId, SecondaryMarketOfferCancelError secondaryMarketOfferCancelError) : base($"Cancel secondary market investment offer id {investmentId} failed with {secondaryMarketOfferCancelError.ToString()}")
        { }

        protected CancelSecondartMarketOfferException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
