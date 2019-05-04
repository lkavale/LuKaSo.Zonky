using Newtonsoft.Json;

namespace LuKaSo.Zonky.Models.Markets
{
    public class IncreasePrimaryMarketInvestment
    {
        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        public decimal Amount { get; set; }
    }
}
