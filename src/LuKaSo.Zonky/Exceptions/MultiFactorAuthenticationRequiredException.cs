using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class MultiFactorAuthenticationRequiredException : Exception
    {
        public MultiFactorAuthenticationRequiredException()
        { }

        protected MultiFactorAuthenticationRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
