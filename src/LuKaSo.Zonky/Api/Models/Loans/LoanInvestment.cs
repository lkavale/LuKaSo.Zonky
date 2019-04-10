using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Loans
{
    /// <summary>
    /// Loan investment
    /// Observed wierd item in responce {"id":-1,"firstAmount":null,"amount":100.00,"additionalAmount":null,"timeCreated":"2019-03-30T00:04:35.439+01:00","investorId":-1,"loanId":436639,"investorNickname":"Rentiér","status":"ACTIVE"}
    /// Missing in Zonky API blueprint 
    /// </summary>
    public class LoanInvestment
    {
        /// <summary>
        /// Id
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
        /// Id of investor
        /// </summary>
        [JsonProperty("investorId", Required = Required.Always)]
        [Required]
        public int InvestorId { get; set; }

        /// <summary>
        /// Investor's nickname
        /// </summary>
        [JsonProperty("investorNickname", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string InvestorNickname { get; set; }

        /// <summary>
        /// First invested amount
        /// </summary>
        [JsonProperty("firstAmount", Required = Required.Default)]
        public decimal? FirstAmount { get; set; }

        /// <summary>
        /// Total invested amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Additional invested amount
        /// </summary>
        [JsonProperty("additionalAmount", Required = Required.Default)]
        public decimal? AdditionalAmount { get; set; }

        /// <summary>
        /// Time of investment
        /// </summary>
        [JsonProperty("timeCreated", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime TimeCreated { get; set; }

        /// <summary>
        /// Status of investment
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanInvestmentStatus Status { get; set; }
    }
}
