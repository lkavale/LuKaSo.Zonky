using LuKaSo.Zonky.Models.Loans;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LuKaSo.Zonky.Models.Overview
{
    public class RiskPortfolio
    {
        public decimal Unpaid { get; set; }

        public decimal Paid { get; set; }

        public decimal Due { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LoanRating Rating { get; set; }

        public decimal TotalAmount { get; set; }
    }
}