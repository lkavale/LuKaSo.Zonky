namespace LuKaSo.Zonky.Models.Investor
{
    public enum WalletTransactionCategory
    {
        /// <summary>
        /// Deposit
        /// </summary>
        DEPOSIT = 0,

        /// <summary>
        /// Withdraw
        /// </summary>
        WITHDRAW = 1,

        /// <summary>
        /// Investment
        /// </summary>
        INVESTMENT = 2,

        /// <summary>
        /// Investment return
        /// </summary>
        INVESTMENT_RETURN = 3,

        /// <summary>
        /// Secondary market buy
        /// </summary>
        SMP_BUY = 4,

        /// <summary>
        /// Secondary market sell
        /// </summary>
        SMP_SELL = 5,

        /// <summary>
        /// Secondary market transfer
        /// </summary>
        SMP_PRICE_TRANSFER = 6,

        /// <summary>
        /// Secondary market sale fee
        /// </summary>
        SMP_SALE_FEE = 7,

        /// <summary>
        /// Credit
        /// </summary>
        CREDIT = 8,

        /// <summary>
        /// Payment
        /// </summary>
        PAYMENT = 9,

        /// <summary>
        /// Resend
        /// </summary>
        RESEND = 10,

        /// <summary>
        /// Return
        /// </summary>
        RETURN = 11,

        /// <summary>
        /// Credit fee
        /// </summary>
        CREDIT_FEE = 12,

        /// <summary>
        /// Credit fee return
        /// </summary>
        CREDIT_FEE_RETURN = 13,

        /// <summary>
        /// Investment fee
        /// </summary>
        INVESTMENT_FEE = 14,

        /// <summary>
        /// Investment fee return
        /// </summary>
        INVESTMENT_FEE_RETURN = 15,

        /// <summary>
        /// Collection fee
        /// </summary>
        COLLECTION_FEE = 16,

        /// <summary>
        /// Interest
        /// </summary>
        INTEREST = 17,

        /// <summary>
        /// Ignore
        /// </summary>
        IGNORE = 18,

        /// <summary>
        /// Unspecified incomming transaction
        /// </summary>
        UNSPECIFIED_IN = 19,

        /// <summary>
        /// Unspecified outcomming transaction
        /// </summary>
        UNSPECIFIED_OUT = 20,
    }
}
