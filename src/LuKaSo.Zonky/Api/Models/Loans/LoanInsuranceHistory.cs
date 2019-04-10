using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Loans
{
    public class LoanInsuranceHistory
    {
        /// <summary>
        /// Period from
        /// </summary>
        [JsonProperty("policyPeriodFrom", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime PolicyPeriodFrom { get; set; }

        /// <summary>
        /// Period to
        /// </summary>
        [JsonProperty("policyPeriodTo", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public DateTime PolicyPeriodTo { get; set; }
    }
}
