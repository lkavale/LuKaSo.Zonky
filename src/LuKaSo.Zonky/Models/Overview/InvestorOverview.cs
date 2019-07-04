using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuKaSo.Zonky.Models.Overview
{
    public class InvestorOverview
    {
        /// <summary>
        /// Overview generated
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Is investor superinvestor
        /// </summary>
        public bool IsSuperInvestor { get; set; }

        /// <summary>
        /// Current profitability
        /// </summary>
        public double CurrentProfitability { get; set; }

        /// <summary>
        /// Expected profitability
        /// </summary>
        public double ExpectedProfitability { get; set; }

        /// <summary>
        /// Profitability
        /// </summary>
        public double Profitability { get; set; }

        /// <summary>
        /// Current overview
        /// </summary>
        public CurrentOverview CurrentOverview { get; set; }

        public OverallOverview OverallOverview { get; set; }

        [JsonIgnore]
        public object SuperInvestorOverview { get; set; }

        [JsonIgnore]
        public object OverallPortfolio { get; set; }

        [JsonIgnore]
        public List<object> CashFlow { get; set; }

        [JsonIgnore]
        public List<object> ExpectedPayments { get; set; }

        public List<RiskPortfolio> RiskPortfolio { get; set; }
    }
}
