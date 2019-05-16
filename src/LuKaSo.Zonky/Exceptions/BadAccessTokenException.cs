using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class BadAccessTokenException : Exception
    {
        public BadAccessTokenException()
        {
        }

        protected BadAccessTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
