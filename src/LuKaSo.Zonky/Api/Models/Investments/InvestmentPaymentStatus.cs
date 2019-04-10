namespace LuKaSo.Zonky.Api.Models.Investments
{
    public enum InvestmentPaymentStatus
    {
        /// <summary>
        /// Ok
        /// </summary>
        OK = 0,

        /// <summary>
        /// Due
        /// </summary>
        DUE = 1,

        /// <summary>
        /// Covered
        /// </summary>
        COVERED = 2,

        /// <summary>
        /// Not covered
        /// </summary>
        NOT_COVERED = 3,

        /// <summary>
        /// Paid off
        /// </summary>
        PAID_OFF = 4,

        /// <summary>
        /// Canceled
        /// </summary>
        CANCELED = 5,

        /// <summary>
        /// Written off
        /// </summary>
        WRITTEN_OFF = 6,

        /// <summary>
        /// Paid
        /// </summary>
        PAID = 7,
    }
}
