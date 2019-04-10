using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Investments
{
    public class InvestmentEvent
    {
        /// <summary>
        /// Id of a related loan
        /// </summary>
        [JsonProperty("loanId", Required = Required.Always)]
        [Required]
        public int LoanId { get; set; }

        /// <summary>
        /// Business code
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("businessCode")]
        public InvestmentEventBusinessCode BusinessCode { get; set; }

        /// <summary>
        /// Date to
        /// </summary>
        [JsonProperty("dateFrom")]
        public ZonkyDateTime DateFrom { get; set; }

        /// <summary>
        /// Date from
        /// </summary>
        [JsonProperty("dateTo")]
        public ZonkyDateTime DateTo { get; set; }

        /// <summary>
        /// Metadata
        /// </summary>
        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        [JsonProperty("publicNote")]
        public string PublicNote { get; set; }
    }
}
