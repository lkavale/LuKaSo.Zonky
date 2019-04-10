using LuKaSo.Zonky.Api.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Investor
{
    public class NotificationLinkParameter
    {
        /// <summary>
        /// Id of a related loan
        /// </summary>
        [JsonProperty("loanId", Required = Required.Always)]
        [Required]
        public int LoanId { get; set; }

        /// <summary>
        /// Borrower's nickname
        /// </summary>
        [JsonProperty("borrowerName", Required = Required.Default)]
        [Required(AllowEmptyStrings = true)]
        public string NickName { get; set; }

        /// <summary>
        /// Is female, Zonky qualified method how specify gender
        /// </summary>
        [JsonProperty("is_female", Required = Required.Default)]
        public bool? IsFemale { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Default)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Loan amount
        /// </summary>
        [JsonProperty("loan_amount", Required = Required.Default)]
        [JsonConverter(typeof(FormatedCurrencyConverter))]
        public decimal? LoanAmount { get; set; }

        /// <summary>
        /// Amount to pay
        /// </summary>
        [JsonProperty("amount_to_pay", Required = Required.Default)]
        [JsonConverter(typeof(FormatedCurrencyConverter))]
        public decimal? AmountToPay { get; set; }
    }
}
