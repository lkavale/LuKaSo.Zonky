using System;

namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Expected daily payments
    /// </summary>
    public class ExpectedPaymentsDaily
    {
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// Principal expected
        /// </summary>
        public decimal PrincipalAmount { get; set; }

        /// <summary>
        /// Interest expected
        /// </summary>
        public decimal InterestAmount { get; set; }

        /// <summary>
        /// Penalty expected
        /// </summary>
        public decimal PenaltyAmount { get; set; }
    }
}
