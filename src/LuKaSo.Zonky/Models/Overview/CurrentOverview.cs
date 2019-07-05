namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Current investor overview
    /// </summary>
    public class CurrentOverview
    {
        /// <summary>
        ///  Active investment inverest left (interest to pay + dued)
        /// </summary>
        public decimal InterestLeft { get; set; }

        /// <summary>
        /// Active investment interest dued
        /// </summary>
        public decimal InterestLeftDue { get; set; }

        /// <summary>
        /// Active investment interest to pay
        /// </summary>
        public decimal InterestLeftToPay { get; set; }

        /// <summary>
        /// Active investment interest paid
        /// </summary>
        public decimal InterestPaid { get; set; }

        /// <summary>
        /// Active investment interest total (interest paid + left)
        /// </summary>
        public decimal InterestPlanned { get; set; }

        /// <summary>
        /// Active investment count
        /// </summary>
        public decimal InvestmentCount { get; set; }

        /// <summary>
        /// Active investment penalty paid
        /// </summary>
        public decimal PenaltyPaid { get; set; }

        /// <summary>
        /// Active investment principal left (principal to pay + dued)
        /// </summary>
        public decimal PrincipalLeft { get; set; }

        /// <summary>
        /// Active investment principal dued
        /// </summary>
        public decimal PrincipalLeftDue { get; set; }

        /// <summary>
        /// Active investment principal to pay
        /// </summary>
        public decimal PrincipalLeftToPay { get; set; }

        /// <summary>
        /// Active investment principal paid
        /// </summary>
        public decimal PrincipalPaid { get; set; }

        /// <summary>
        /// Active investment amount
        /// </summary>
        public decimal TotalInvestment { get; set; }
    }
}