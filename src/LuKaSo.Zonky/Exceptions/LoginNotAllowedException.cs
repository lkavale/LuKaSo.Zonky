using System;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class LoginNotAllowedException : Exception
    {
        public LoginNotAllowedException()
        { }

        protected LoginNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
