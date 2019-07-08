using System;
using System.Collections.ObjectModel;

namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Investor overview
    /// </summary>
    public class InvestorOverview
    {
        /// <summary>
        /// Overview generated
        /// </summary>
        public DateTime Timestamp { get; set; }

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
        /// Current investor overview
        /// </summary>
        public CurrentOverview CurrentOverview { get; set; }

        /// <summary>
        /// Overall investor overview
        /// </summary>
        public OverallOverview OverallOverview { get; set; }

        /// <summary>
        /// Super-investor overview
        /// </summary>
        public SuperInvestorOverview SuperInvestorOverview { get; set; }

        /// <summary>
        /// Cashflow
        /// </summary>
        public Collection<CashflowMonthly> CashFlow { get; set; }

        /// <summary>
        /// Expected payments
        /// </summary>
        public Collection<ExpectedPaymentsDaily> ExpectedPayments { get; set; }

        /// <summary>
        /// Zonky rating grouped portfolio overview
        /// </summary>
        public Collection<RiskPortfolio> RiskPortfolio { get; set; }

        /// <summary>
        /// Overall portfolio
        /// </summary>
        //[Obsolete("Possible obsolote, API return all fields zero")]
        //public object OverallPortfolio { get; set; }
    }
}
