namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Current investor overview
    /// </summary>
    public class CurrentOverview
    {
        /// <summary>
        ///  Inverest left (interest to pay + dued)
        /// </summary>
        public decimal InterestLeft { get; set; }

        /// <summary>
        /// Interest dued
        /// </summary>
        public decimal InterestLeftDue { get; set; }

        /// <summary>
        /// Interest to pay
        /// </summary>
        public decimal InterestLeftToPay { get; set; }

        /// <summary>
        /// Interest paid
        /// </summary>
        public decimal InterestPaid { get; set; }

        /// <summary>
        /// Interest total (interest paid + left)
        /// </summary>
        public decimal InterestPlanned { get; set; }

        /// <summary>
        /// Active investment count
        /// </summary>
        public decimal InvestmentCount { get; set; }

        /// <summary>
        /// Penalty paid
        /// </summary>
        public decimal PenaltyPaid { get; set; }

        /// <summary>
        /// Principal left (principal to pay + dued)
        /// </summary>
        public decimal PrincipalLeft { get; set; }

        /// <summary>
        /// Principal dued
        /// </summary>
        public decimal PrincipalLeftDue { get; set; }

        /// <summary>
        /// Principal to pay
        /// </summary>
        public decimal PrincipalLeftToPay { get; set; }

        /// <summary>
        /// Principal paid
        /// </summary>
        public decimal PrincipalPaid { get; set; }

        /// <summary>
        /// Total investment amount
        /// </summary>
        public decimal TotalInvestment { get; set; }
    }
}