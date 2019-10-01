using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class MultiFactorAuthenticationRequiredException : Exception
    {
        public Guid MfaToken { get; }

        public MultiFactorAuthenticationRequiredException(Guid mfaToken)
        {
            MfaToken = mfaToken;
        }

        protected MultiFactorAuthenticationRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
