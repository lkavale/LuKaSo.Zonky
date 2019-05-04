using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Loans
{
    public class Loan
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the loan
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        /// <summary>
        /// Borrower's nickname
        /// </summary>
        [JsonProperty("nickName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string NickName { get; set; }

        /// <summary>
        /// Short story of the loan. Usually some story about the purpose of a loan that attracts investors
        /// </summary>
        [JsonProperty("story", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Story { get; set; }

        /// <summary>
        /// Date of the loan publication on marketplace
        /// </summary>
        [JsonProperty("datePublished", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime DatePublished { get; set; }

        /// <summary>
        /// Deadline of the loan
        /// Only loans with deadline after actual date and time are visible in the marketplace
        /// </summary>
        [JsonProperty("deadline", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// The type of the income that is set as primary
        /// </summary>
        [JsonProperty("mainIncomeType", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MainIncomeType MainIncomeType { get; set; }

        /// <summary>
        /// Purpose of the loan (code from the register)
        /// </summary>
        [JsonProperty("purpose", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public LoanPurpose Purpose { get; set; }

        /// <summary>
        /// Rating of the loan
        /// Rating is a risk category for the loan determined after scoring
        /// Removed after Zonky changed rating to loan rate
        /// </summary>
        /*[JsonProperty("rating", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanRating Rating { get; set; }*/

        /// <summary>
        /// The code of the region
        /// </summary>
        [JsonProperty("region", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public Region Region { get; set; }

        /// <summary>
        /// Interest rate for investors
        /// </summary>
        [JsonProperty("interestRate", Required = Required.Always)]
        [Required]
        public decimal InterestRate { get; set; }

        /// <summary>
        /// True if the loan has been published on the marketplace, false otherwise
        /// </summary>
        [JsonProperty("published", Required = Required.Always)]
        [Required]
        public bool Published { get; set; }

        /// <summary>
        /// True if the loan is covered, false otherwise
        /// </summary>
        [JsonProperty("covered", Required = Required.Always)]
        [Required]
        public bool Covered { get; set; }

        /// <summary>
        /// The count of investments attached to this loan
        /// </summary>
        [JsonProperty("investmentsCount", Required = Required.Always)]
        [Required]
        public int InvestmentsCount { get; set; }

        /// <summary>
        /// The amount offered to and accepted by borrower
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Loan term (in months)
        /// </summary>
        [JsonProperty("termInMonths", Required = Required.Always)]
        [Required]
        public int TermInMonths { get; set; }

        /// <summary>
        /// True if loan has been topped, false otherwise
        /// </summary>
        [JsonProperty("topped", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Topped { get; set; }

        /// <summary>
        /// Loan annuity including insurance premium (if insured)
        /// </summary>
        [JsonProperty("annuityWithInsurance", Required = Required.Always)]
        [Required]
        public decimal AnnuityWithInsurance { get; set; }

        /// <summary>
        /// Insurance is active
        /// </summary>
        [JsonProperty("insuranceActive", Required = Required.Always)]
        [Required]
        public bool InsuranceActive { get; set; }

        /// <summary>
        /// The count of questions attached to this loan
        /// </summary>
        [JsonProperty("questionsCount", Required = Required.Always)]
        [Required]
        public int QuestionsCount { get; set; }

        /// <summary>
        /// The remaining amount available for investment, users without a reservation can invest only amount equal to `remainingInvestment - reservedAmount`
        /// </summary>
        [JsonProperty("remainingInvestment", Required = Required.Always)]
        [Required]
        public decimal RemainingInvestment { get; set; }

        /// <summary>
        /// The amount reserved for investors with a reservation for given loan, when not zero, users without reservations can invest only amount equal to `remainingInvestment - reservedAmount`
        /// </summary>
        [JsonProperty("reservedAmount", Required = Required.Always)]
        [Required]
        public decimal ReservedAmount { get; set; }

        /// <summary>
        /// Revenue rate for investors (loan interest rate - investment fee)
        /// </summary>
        [JsonProperty("revenueRate", Required = Required.Always)]
        [Required]
        public decimal RevenueRate { get; set; }

        /// <summary>
        /// TODO:
        /// Calc at get, exclude from API
        /// Current investment rate
        /// </summary>
        [JsonProperty("investmentRate", Required = Required.Always)]
        public decimal InvestmentRate { get; set; }

        /// <summary>
        /// All insurance intervals
        /// </summary>
        [JsonProperty("insuranceHistory", Required = Required.Always)]
        [Required]
        public ICollection<LoanInsuranceHistory> InsuranceHistory { get; set; }

        /// <summary>
        /// Photos attached to this loan
        /// </summary>
        [JsonProperty("photos", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<LoanPhoto> Photos { get; set; }

        /// <summary>
        /// The permalink for the loan
        /// 
        /// The link refers to the official Zonky website with detail of the loan.
        /// It is intended to be used by 3rd party applications,
        /// which can simply use the obtained link instead of generating a custom one.
        /// The main advantage of this link is guarantee that it will be valid in long term.
        /// </summary>
        [JsonProperty("url", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}
