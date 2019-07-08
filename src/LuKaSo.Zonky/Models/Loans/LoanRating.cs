namespace LuKaSo.Zonky.Models.Loans
{
    /// <summary>
    /// Loan rating is depricated, but many contracts still using it
    /// </summary>
    public enum LoanRating
    {
        /// <summary>
        /// 2,99%
        /// </summary>
        AAAAAA,

        /// <summary>
        /// 3,99%, old A** rating
        /// </summary>
        AAAAA,

        /// <summary>
        /// 4,99%, old A* rating
        /// </summary>
        AAAA,

        /// <summary>
        /// 5,99%, old A++ rating
        /// </summary>
        AAA,

        /// <summary>
        /// 6,99%
        /// </summary>
        AAE,

        /// <summary>
        /// 8,49%, old A+ rating
        /// </summary>
        AA,

        /// <summary>
        /// 9,49%
        /// </summary>
        AE,

        /// <summary>
        /// 10,99%, old A rating
        /// </summary>
        A,

        /// <summary>
        /// 13,49%, old B rating
        /// </summary>
        B,

        /// <summary>
        /// 15,49%, old C rating
        /// </summary>
        C,

        /// <summary>
        /// 19,99%, old D rating
        /// </summary>
        D
    }
}
