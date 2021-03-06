﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Markets
{
    public class SecondaryMarketOfferSell
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

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Sell request of investment id {InvestmentId} with fee {FeeAmount}, remaining principal {RemainingPrincipal}.";
        }
    }
}
