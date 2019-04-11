using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class SecondaryMarketOfferCancelError
    {
        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("error", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public SecondaryMarketOfferCancelErrorType Error { get; set; }

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Secondary market investment offer cancelation failed with {Error.ToString()} - {Description}";
        }
    }
}
