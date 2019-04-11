using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Markets
{
    public class PrimaryMarketInvestment
    {
        /// <summary>
        /// Loan id
        /// </summary>
        [JsonProperty("loanId", Required = Required.Always)]
        [Required]
        public int LoanId { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Captcha response
        /// </summary>
        [JsonProperty("captcha_response", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string CaptchaResponse { get; set; }

        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Primary market buy participation of loan id {LoanId} with amount {Amount}, captcha responce {CaptchaResponse}";
        }
    }
}
