using System;

namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Cashflow monthly
    /// </summary>
    public class CashflowMonthly
    {
        /// <summary>
        /// Month
        /// </summary>
        public DateTime Month { get; set; }

        /// <summary>
        /// Instalment amount expected
        /// </summary>
        public decimal InstalmentAmount { get; set; }

        /// <summary>
        /// Principal received
        /// </summary>
        public decimal PrincipalPaid { get; set; }

        /// <summary>
        /// Interest received
        /// </summary>
        public decimal InterestPaid { get; set; }

        /// <summary>
        /// Penalty received
        /// </summary>
        public decimal PenaltyPaid { get; set; }
    }
}
