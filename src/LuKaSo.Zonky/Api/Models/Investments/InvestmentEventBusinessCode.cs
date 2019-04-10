﻿namespace LuKaSo.Zonky.Api.Models.Investments
{
    public enum InvestmentEventBusinessCode
    {
        LC_OTHER,
        LC_CREDIT_REDEMPTION,
        LC_CREDIT_REDEMPTION_CALL,

        AUTO_LOAN_OVERDUE_INST_3RD_PENALTY_WARNING,
        AUTO_LOAN_OVERDUE_INST_DEFAULT_WARNING,
        AUTO_LOAN_OVERDUE_INST_3RD_INST_APROACHING,
        AUTO_LOAN_OVERDUE_INST_AFTER_2ND_PENALTY_WARNING,
        AUTO_LOAN_OVERDUE_INST_JUDICIAL_ENFORCMENT_WARNING,
        AUTO_LOAN_OVERDUE_INST_2ND_INST_APROACHING,
        AUTO_LOAN_OVERDUE_INST_PENALTY_WARNING,

        AUTO_LOAN_DELAY_BORROWER,
        AUTO_BORROWER_RECOVERED,
        AUTO_PAYMENT_PAIRED
    }
}
