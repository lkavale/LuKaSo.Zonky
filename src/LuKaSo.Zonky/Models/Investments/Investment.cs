using LuKaSo.Zonky.Models.Loans;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Investments
{
    public class Investment
    {
        /// <summary>
        /// Id of an investment
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id of a related loan
        /// </summary>
        [JsonProperty("loanId", Required = Required.Always)]
        [Required]
        public int LoanId { get; set; }

        /// <summary>
        /// Borrower's nickname
        /// </summary>
        [JsonProperty("nickName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string NickName { get; set; }

        /// <summary>
        /// Borrower's firstname
        /// Property is documented in API but not served for investprs accounts, so I exclude it
        /// </summary>
        /*[JsonProperty("firstName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string FirstName { get; set; }*/

        /// <summary>
        /// Borrower's surname
        /// Property is documented in API but not served for investprs accounts, so I exclude it
        /// </summary>
        /*[JsonProperty("surname", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Surname { get; set; }*/

        /// <summary>
        /// Name of a loan
        /// </summary>
        [JsonProperty("loanName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string LoanName { get; set; }

        /// <summary>
        /// Date of investment
        /// </summary>
        [JsonProperty("investmentDate", Required = Required.Default)]
        public DateTime? InvestmentDate { get; set; }

        /// <summary>
        /// Rating of the loan
        /// Removed after Zonky rating update
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
        /// Amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Investment amount before eventual investment increase
        /// </summary>
        [JsonProperty("firstAmount", Required = Required.Always)]
        [Required]
        public decimal FirstAmount { get; set; }

        /// <summary>
        /// Purchase amount when the investment has been purchased on secondary marketplace
        /// </summary>
        [JsonProperty("purchasePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Repaid principal amount
        /// </summary>
        [JsonProperty("paid", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal Paid { get; set; }

        /// <summary>
        /// Principal amount that is left for repayment
        /// </summary>
        [JsonProperty("toPay", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal ToPay { get; set; }

        /// <summary>
        /// Due date of the next instalment
        /// </summary>
        [JsonProperty("nextPaymentDate", Required = Required.Default)]
        public DateTime? NextPaymentDate { get; set; }

        /// <summary>
        /// Payment status
        /// </summary>
        [JsonProperty("paymentStatus", Required = Required.Default)]
        [JsonConverter(typeof(StringEnumConverter))]
        public InvestmentPaymentStatus? PaymentStatus { get; set; }

        /// <summary>
        /// Number of days after a due date of the oldest unpaid instalment
        /// </summary>
        [JsonProperty("legalDpd", Required = Required.Default)]
        public int? LegalDpd { get; set; }

        /// <summary>
        /// Total due amount
        /// </summary>
        [JsonProperty("amountDue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountDue { get; set; }

        /// <summary>
        /// Original number of loan instalments (at the time of investment)
        /// </summary>
        [JsonProperty("loanTermInMonth", Required = Required.Always)]
        [Required]
        public int LoanTermInMonth { get; set; }

        /// <summary>
        /// Total sum of paid interest
        /// </summary>
        [JsonProperty("paidInterest", Required = Required.Always)]
        [Required]
        public decimal PaidInterest { get; set; }

        /// <summary>
        /// Total sum of due interest
        /// </summary>
        [JsonProperty("dueInterest", Required = Required.Always)]
        [Required]
        public decimal DueInterest { get; set; }

        /// <summary>
        /// Total sum of paid principal
        /// </summary>
        [JsonProperty("paidPrincipal", Required = Required.Always)]
        [Required]
        public decimal PaidPrincipal { get; set; }

        /// <summary>
        /// Total sum of due principal
        /// </summary>
        [JsonProperty("duePrincipal", Required = Required.Always)]
        [Required]
        public decimal DuePrincipal { get; set; }

        /// <summary>
        /// Total sum of remaining principal that has not been repaid yet
        /// </summary>
        [JsonProperty("remainingPrincipal", Required = Required.Default)]
        public decimal? RemainingPrincipal { get; set; }

        /// <summary>
        /// Paid penalty
        /// </summary>
#warning Not listed in Zonky API
        [JsonProperty("paidPenalty")]
        [Required]
        public decimal PaidPenalty { get; set; }

        /// <summary>
        /// The amount for which the investment has been sold on secondary marketplace (if it has been sold)
        /// </summary>
        [JsonProperty("smpSoldFor", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SmpSoldFor { get; set; }

        /// <summary>
        /// Total amount of interest that is expected to be received from the investment
        /// </summary>
        [JsonProperty("expectedInterest", Required = Required.Always)]
        [Required]
        public decimal ExpectedInterest { get; set; }

        /// <summary>
        /// Current number of loan instalments
        /// </summary>
        [JsonProperty("currentTerm", Required = Required.Default)]
        public int? CurrentTerm { get; set; }

        /// <summary>
        /// True if an investment can be offered for purchase on the secondary marketplace
        /// </summary>
        [JsonProperty("canBeOffered", Required = Required.Always)]
        [Required]
        public bool CanBeOffered { get; set; }

        /// <summary>
        /// True if an investment has been offered for purchase on the secondary marketplace
        /// </summary>
        [JsonProperty("onSmp", Required = Required.Always)]
        [Required]
        public bool IsOnSmp { get; set; }

        /// <summary>
        /// Insurance is active
        /// </summary>
        [JsonProperty("insuranceActive", Required = Required.Always)]
        [Required]
        public bool InsuranceActive { get; set; }

        /// <summary>
        /// True if an investment has been offered for purchase on the secondary marketplace at the current moment or sometimes in the past
        /// </summary>
        [JsonProperty("smpRelated", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool SmpRelated { get; set; }

        /// <summary>
        /// Has collection history
        /// </summary>
#warning Not listed in Zonky API
        [JsonProperty("hasCollectionHistory")]
        [Required]
        public bool HasCollectionHistory { get; set; }

        /// <summary>
        /// Number of months left to total repayment
        /// </summary>
        [JsonProperty("remainingMonths", Required = Required.Always)]
        [Required]
        public int RemainingMonths { get; set; }

        /// <summary>
        /// Investment status
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public InvestmentStatus LoanStatus { get; set; }

        /// <summary>
        /// The date when an investment was created
        /// </summary>
        [JsonProperty("timeCreated", Required = Required.Default)]
        public DateTime? TimeCreated { get; set; }

        /// <summary>
        /// The date of sale on a secondary marketplace (if sold)
        /// </summary>
        [JsonProperty("activeTo", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ActiveTo { get; set; }

        /// <summary>
        /// Fee that has been taken for selling an investment on a secondary marketplace (if sold)
        /// </summary>
        [JsonProperty("smpFee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SmpFee { get; set; }

        /// <summary>
        /// Instalments are postponed due to insurance event
        /// </summary>
        [JsonProperty("instalmentPostponement", Required = Required.Always)]
        [Required]
        public bool InstalmentPostponement { get; set; }

        /// <summary>
        /// All insurance intervals
        /// </summary>
        [JsonProperty("insuranceHistory", Required = Required.Always)]
        [Required]
        public ICollection<LoanInsuranceHistory> InsuranceHistory { get; set; }
    }
}
