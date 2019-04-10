using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Investor
{
    public class WalletTransaction
    {
        /// <summary>
        /// Id of a transaction
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id of the related investment
        /// </summary>
        [JsonProperty("investmentId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? InvestmentId { get; set; }

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
        [JsonProperty("transactionDate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Name of the related loan
        /// </summary>
        [JsonProperty("loanName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LoanName { get; set; }

        /// <summary>
        /// Name of the related borrower
        /// </summary>
        [JsonProperty("nickName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        /// <summary>
        /// Transaction category
        /// </summary>
        [JsonProperty("category", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public WalletTransactionCategory Category { get; set; }

        /// <summary>
        /// Orientation of the transaction
        /// </summary>
        [JsonProperty("orientation", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public WalletTransactionOrientation Orientation { get; set; }

        /// <summary>
        /// Custom message
        /// </summary>
        [JsonProperty("customMessage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string CustomMessage { get; set; }
    }
}
