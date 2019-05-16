using LuKaSo.Zonky.Models.Login;
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class BadLoginException : Exception
    {
        public BadLoginException(HttpResponseMessage message, User user) : base($"Bad login information for user {user.Name}. \r\n Server return \r\n {message.ToString()}")
        { }

        public BadLoginException(User user) : base($"Bad login information for user {user.Name}.")
        { }

        protected BadLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
