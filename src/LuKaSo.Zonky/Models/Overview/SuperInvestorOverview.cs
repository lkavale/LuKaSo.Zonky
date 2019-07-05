using System.Collections.ObjectModel;

namespace LuKaSo.Zonky.Models.Overview
{
    /// <summary>
    /// Super-investor monthly overview
    /// </summary>
    public class SuperInvestorOverview
    {
        /// <summary>
        /// Is investor superinvestor
        /// </summary>
        public bool IsSuperInvestor { get; set; }

        /// <summary>
        /// Super-investor monthly overview
        /// </summary>
        public Collection<SuperInvestorMonthlyOverview> MonthlyOverview { get; set; }
    }
}
