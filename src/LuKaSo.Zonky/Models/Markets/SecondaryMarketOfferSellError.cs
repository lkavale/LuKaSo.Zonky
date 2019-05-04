using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Markets
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

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Sell investment on secondary market failed {Error.ToString()} - {Description}";
        }
    }
}
