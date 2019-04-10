using LuKaSo.Zonky.Api.Models.Loans;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class SecondaryMarketOffer
    {
        /// <summary>
        /// ID of an investment offer
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Not listed in Zonky API, probably investor Id
        /// </summary>
#warning Not listed in Zonky API
        [JsonProperty("investmentId", Required = Required.Always)]
        [Required]
        public int InvestmentId { get; set; }

        /// <summary>
        /// Id of a related loan
        /// </summary>
        [JsonProperty("loanId", Required = Required.Always)]
        [Required]
        public int LoanId { get; set; }

        /// <summary>
        /// Id of an investor offering his investment
        /// </summary>
        [JsonProperty("userId", Required = Required.Always)]
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Loan purpose, transfered as int
        /// </summary>
#warning Not listed in Zonky API
        [JsonProperty("purpose")]
        [Required]
        public LoanPurpose Purpose { get; set; }

        /// <summary>
        /// Client main income type
        /// </summary>
#warning Not listed in Zonky API
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("incomeType")]
        public MainIncomeType MainIncomeType { get; set; }

        /// <summary>
        /// Name of a loan
        /// </summary>
        [JsonProperty("loanName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string LoanName { get; set; }

        /// <summary>
        /// Rating of the loan
        /// TODO:
        /// Remove after Zonky rating update
        /// </summary>
        [JsonProperty("rating", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanRating Rating { get; set; }

        /// <summary>
        /// Interest rate for investors
        /// </summary>
        [JsonProperty("interestRate", Required = Required.Always)]
        [Required]
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Number of remaining unpaid instalments
        /// </summary>
        [JsonProperty("remainingInstalmentCount", Required = Required.Always)]
        [Required]
        public int RemainingInstalmentCount { get; set; }

        /// <summary>
        /// Number of instalments at the time when a loan was first published on primary marketplace
        /// </summary>
        [JsonProperty("originalInstalmentCount", Required = Required.Always)]
        [Required]
        public int OriginalInstalmentCount { get; set; }

        /// <summary>
        /// Remaining principal is the unreturned part of the whole investment and at the same time it's the amount for which an investment can be purchased
        /// </summary>
        [JsonProperty("remainingPrincipal", Required = Required.Always)]
        [Required]
        public decimal RemainingPrincipal { get; set; }

        /// <summary>
        /// Purchase offer on secondary marketplace will be cancelled after this deadline
        /// </summary>
        [JsonProperty("deadline", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// The due date of the next instalment
        /// </summary>
        [JsonProperty("nextPaymentDate", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime NextPaymentDate { get; set; }

        /// <summary>
        /// Will exceed loan investment limit
        /// </summary>
#warning Not listed in Zonky API
        [JsonProperty("willExceedLoanInvestmentLimit")]
        public bool? WillExceedLoanInvestmentLimit { get; set; }

    }
}
