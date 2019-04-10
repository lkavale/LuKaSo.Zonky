using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class SecondaryMarketOfferSellError
    {
        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("error", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public SecondaryMarketOfferSellErrorType Error { get; set; }
    }
}
