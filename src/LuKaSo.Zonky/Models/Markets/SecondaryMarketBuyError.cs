using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Markets
{
    public class SecondaryMarketBuyError
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
        public SecondaryMarketBuyErrorType Error { get; set; }

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Secondary market investment buy error {Error.ToString()} - {Description}";
        }
    }
}
