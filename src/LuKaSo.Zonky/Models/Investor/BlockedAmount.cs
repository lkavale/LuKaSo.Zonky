using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Investor
{
    public class BlockedAmount
    {
        /// <summary>
        /// Id of the related loan
        /// </summary>
        [JsonProperty("loanId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? LoanId { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Date of the transaction
        /// </summary>
        [JsonProperty("dateStart", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Name of the related loan
        /// </summary>
        [JsonProperty("loanName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LoanName { get; set; }

        /// <summary>
        /// Transaction category
        /// </summary>
        [JsonProperty("category", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public WalletTransactionCategory Category { get; set; }
    }
}
