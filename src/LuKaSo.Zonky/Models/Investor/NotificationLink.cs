using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Investor
{
    public class NotificationLink
    {
        /// <summary>
        /// Notification link type
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public NotificationLinkType Type { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        [JsonProperty("params", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public NotificationLinkParameter Parameter { get; set; }
    }
}
