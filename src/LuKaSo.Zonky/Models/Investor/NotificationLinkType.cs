namespace LuKaSo.Zonky.Models.Investor
{
    public enum NotificationLinkType
    {
        /// <summary>
        /// Default type
        /// </summary>
        NOOP,

        /// <summary>
        /// Loan overdue
        /// </summary>
        LOAN_DELAY_INVESTOR,

        /// <summary>
        /// The borrower has healed
        /// </summary>
        BORROWER_HEAL,

        /// <summary>
        /// Early repayment, extraordinary repayment
        /// </summary>
        LOAN_PREPAYMENT,

#warning Not listed in Zonky API
        LOAN_PAID_OFF_DEBT,

#warning Not listed in Zonky API
        LOAN_PENALTY_PAID,

#warning Not listed in Zonky API
        LOAN_PAID_OFF_INSOLVENCY,

        /// <summary>
        /// Adoption of the money in the wallet
        /// </summary>
        WALLET_INCOMING,

        /// <summary>
        /// Trading of the investment
        /// </summary>
        LOAN_SUCCESS,

        /// <summary>
        /// The answer to your question
        /// </summary>
        LOAN_ANSWER_SAVE,

#warning Not listed in Zonky API
        LOAN_PAID_OFF_DEATH,

#warning Not listed in Zonky API
        LOAN_PAID_OFF,

#warning Not listed in Zonky API
        SMP_SOLD,

        /// <summary>
        /// Long-time application
        /// </summary>
        LOAN_APPLIED_NO_ACTIVATE,

        /// <summary>
        /// Application was approved
        /// </summary>
        APPLICATION_APPROVE,

        /// <summary>
        /// Documents required to approve an application
        /// </summary>
        APPLICATION_DOCUMENTS,

        /// <summary>
        /// Documents required to approve an application (second notification)
        /// </summary>
        APPLICATION_DOCUMENTS_SECOND,

        /// <summary>
        /// Print agreement sent to borrower
        /// </summary>
        APPLICATION_AGREEMENT,

        /// <summary>
        /// Application added to marketplace
        /// </summary>
        APPLICATION_MARKETPLACE,

        /// <summary>
        /// Loan was published
        /// </summary>
        LOAN_PUBLISHED,

        /// <summary>
        /// Loan is covered and money is being send. This was replaced by LOAN_COVERED
        /// </summary>
        LOAN_SEND_MONEY_BORROWER,

        /// <summary>
        ///  Loan was covered by investors
        /// </summary>
        LOAN_COVERED,

        /// <summary>
        ///  Question from chat
        /// </summary>
        LOAN_QUESTION_SAVE,

        /// <summary>
        ///  The borrower fell into delinquency
        /// </summary>
        LOAN_DELAY_BORROWER,

        /// <summary>
        /// Successful repayment of the loan
        /// </summary>
        LOAN_REPAYMENT_COMPLETED_BORROWER,

        /// <summary>
        ///  The repayment term is approaching
        /// </summary>
        LOAN_DEADLINE_INSTALMENT,

        /// <summary>
        /// Different amounts of the last installment
        /// </summary>
        LOAN_DIFFERENT_LAST_INSTALMENT,

        /// <summary>
        ///  Congratulations to repayment of half loan
        /// </summary>
        LOAN_PAYMENT_HALF,

        /// <summary>
        /// Non-trading of investments
        /// </summary>
        LOAN_CANCEL,

        /// <summary>
        /// To borrower: approved loan is still unsigned
        /// </summary>
        LOAN_UNSIGNED,

        /// <summary>
        /// Investor applied
        /// </summary>
        REQUEST_INVESTOR_WELCOME,

        /// <summary>
        ///  The instalment was paid
        /// </summary>
        INSTALMENT_PAID
    }
}
