using LuKaSo.Zonky.Models.Loans;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Overview
{
    public class RiskPortfolio
    {
        /// <summary>
        /// Unpaid principal
        /// </summary>
        public decimal Unpaid { get; set; }

        /// <summary>
        /// Paid principal
        /// </summary>
        public decimal Paid { get; set; }

        /// <summary>
        /// Due principal
        /// </summary>
        public decimal Due { get; set; }

        /// <summary>
        /// Loan rating
        /// </summary>
        [JsonProperty("rating", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoanRating Rating { get; set; }

        /// <summary>
        /// Total principal
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
