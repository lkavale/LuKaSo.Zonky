using System;

namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Super-investor monthly overview
    /// </summary>
    public class SuperInvestorMonthlyOverview
    {
        /// <summary>
        /// Month
        /// </summary>
        public DateTime Month { get; set; }

        /// <summary>
        /// Are conditions met for month
        /// </summary>
        public bool ConditionsMet { get; set; }

        /// <summary>
        /// Amount sent to wallet
        /// </summary>
        public decimal WalletAmount { get; set; }

        /// <summary>
        /// Amount required to send to wallet 
        /// </summary>
        public decimal WalletRequired { get; set; }

        /// <summary>
        /// Investments count
        /// </summary>
        public int Investments { get; set; }

        /// <summary>
        /// Investments count required
        /// </summary>
        public int InvestmentsRequired { get; set; }
    }
}
