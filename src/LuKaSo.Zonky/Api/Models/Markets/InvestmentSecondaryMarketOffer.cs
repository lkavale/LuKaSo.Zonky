using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class InvestmentSecondaryMarketOffer
    {
        /// <summary>
        /// Id of an investment
        /// </summary>
        [JsonProperty("investmentId", Required = Required.Always)]
        [Required]
        public int InvestmentId { get; set; }

        /// <summary>
        /// Current fee amount
        /// </summary>
        [JsonProperty("feeAmount", Required = Required.Always)]
        [Required]
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// Current remaining principal
        /// </summary>
        [JsonProperty("remainingPrincipal", Required = Required.Always)]
        [Required]
        public decimal RemainingPrincipal { get; set; }
    }
}
