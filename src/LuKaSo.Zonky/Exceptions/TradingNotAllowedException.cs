using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class TradingNotAllowedException : Exception
    {
        public TradingNotAllowedException()
        { }

        protected TradingNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
