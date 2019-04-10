using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LuKaSo.Zonky.Api.Models.Login
{
    public class AuthorizationToken
    {
        /// <summary>
        /// Access token
        /// </summary>
        [JsonProperty("access_token")]
        public Guid AccessToken { get; set; }

        /// <summary>
        /// Token type
        /// </summary>
        [JsonProperty("token_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AuthorizationTokenType TokenType { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        [JsonProperty("refresh_token")]
        public Guid RefreshToken { get; set; }

        /// <summary>
        /// Expires
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Scope
        /// </summary>
        [JsonProperty("scope")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AuthorizationScope Scope { get; set; }
    }
}
