using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException()
        { }

        protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
