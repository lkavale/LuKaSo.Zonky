namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Overall investor overview
    /// </summary>
    public class OverallOverview
    {
        /// <summary>
        /// Total fees paid
        /// </summary>
        public decimal FeesAmount { get; set; }

        /// <summary>
        /// Total fees discount
        /// </summary>
        public decimal FeesDiscount { get; set; }

        /// <summary>
        /// Total interest received
        /// </summary>
        public decimal InterestPaid { get; set; }

        /// <summary>
        /// Total investment count
        /// </summary>
        public decimal InvestmentCount { get; set; }

        /// <summary>
        /// Total net income
        /// </summary>
        public decimal NetIncome { get; set; }

        /// <summary>
        /// Total penalty paid
        /// </summary>
        public decimal PenaltyPaid { get; set; }

        /// <summary>
        /// Total principal lost
        /// </summary>
        public decimal PrincipalLost { get; set; }

        /// <summary>
        /// Total principal paid
        /// </summary>
        public decimal PrincipalPaid { get; set; }

        /// <summary>
        /// Total investment amount
        /// </summary>
        public decimal TotalInvestment { get; set; }
    }
}