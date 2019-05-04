using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LuKaSo.Zonky.Models.Markets
{
    public class SecondaryMarketInvestment
    {
        /// <summary>
        /// The purchase amount, currently it has to be equal to the total value of investment offered for purchase
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Secondary market investment with amount {Amount.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
