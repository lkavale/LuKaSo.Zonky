namespace LuKaSo.Zonky.Models.Investor
{
    public enum WalletTransactionCategory
    {
        /// <summary>
        /// Deposit
        /// </summary>
        DEPOSIT,

        /// <summary>
        /// Withdraw
        /// </summary>
        WITHDRAW,

        /// <summary>
        /// Investment
        /// </summary>
        INVESTMENT,

        /// <summary>
        /// Investment return
        /// </summary>
        INVESTMENT_RETURN,

        /// <summary>
        /// Secondary market buy
        /// </summary>
        SMP_BUY,

        /// <summary>
        /// Secondary market sell
        /// </summary>
        SMP_SELL,

        /// <summary>
        /// Secondary market transfer
        /// </summary>
        SMP_PRICE_TRANSFER,

        /// <summary>
        /// Secondary market sale fee
        /// </summary>
        SMP_SALE_FEE,

        /// <summary>
        /// Credit
        /// </summary>
        CREDIT,

        /// <summary>
        /// Payment
        /// </summary>
        PAYMENT,

        /// <summary>
        /// Resend
        /// </summary>
        RESEND,

        /// <summary>
        /// Return
        /// </summary>
        RETURN,

        /// <summary>
        /// Credit fee
        /// </summary>
        CREDIT_FEE,

        /// <summary>
        /// Credit fee return
        /// </summary>
        CREDIT_FEE_RETURN,

        /// <summary>
        /// Investment fee
        /// </summary>
        INVESTMENT_FEE,

        /// <summary>
        /// Investment fee return
        /// </summary>
        INVESTMENT_FEE_RETURN,

        /// <summary>
        /// Collection fee
        /// </summary>
        COLLECTION_FEE,

        /// <summary>
        /// Interest
        /// </summary>
        INTEREST,

        /// <summary>
        /// Ignore
        /// </summary>
        IGNORE,

        /// <summary>
        /// Unspecified incomming transaction
        /// </summary>
        UNSPECIFIED_IN = 19,

        /// <summary>
        /// Unspecified outcomming transaction
        /// </summary>
        UNSPECIFIED_OUT,

        /// <summary>
        /// Transaction reverted
        /// </summary>
        PAYMENT_REVERT
    }
}
