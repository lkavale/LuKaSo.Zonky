using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LuKaSo.Zonky.Models.Login
{
    public class AuthorizationError
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string Description { get; set; }

        [JsonProperty("error_code")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AuthorizationErrorCode Code { get; set; }

        [JsonProperty("mfa_token")]
        public Guid? MfaToken { get; set; }
    }
}
