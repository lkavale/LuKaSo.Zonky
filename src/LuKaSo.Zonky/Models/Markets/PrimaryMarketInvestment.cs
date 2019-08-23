using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LuKaSo.Zonky.Models.Markets
{
    public class PrimaryMarketInvestment
    {
        /// <summary>
        /// Stringify object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Primary market buy participation of loan id {LoanId} with amount {Amount.ToString("C", CultureInfo.CreateSpecificCulture("cs-CZ"))}, captcha responce {CaptchaResponse}";
        }

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
    }
}
