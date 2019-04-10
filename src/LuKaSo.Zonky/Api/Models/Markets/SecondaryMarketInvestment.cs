using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class SecondaryMarketInvestment
    {
        /// <summary>
        /// The purchase amount, currently it has to be equal to the total value of investment offered for purchase
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }
    }
}
