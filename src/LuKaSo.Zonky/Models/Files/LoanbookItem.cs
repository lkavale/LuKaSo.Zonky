using LuKaSo.Zonky.Attributes;
using System;

namespace LuKaSo.Zonky.Models.Files
{
    [SpreadsheetSheet("data")]
    [SpreadsheetVerticalCoordinate(2)]
    public class LoanbookItem
    {
        [SpreadsheetHorizontalCoordinate("A")]
        public DateTime ReportDate { get; set; }

        [SpreadsheetHorizontalCoordinate("B")]
        public string Id { get; set; }

        [SpreadsheetHorizontalCoordinate("C")]
        public string Region { get; set; }

        [SpreadsheetHorizontalCoordinate("D")]
        public string MainIncomeType { get; set; }

        [SpreadsheetHorizontalCoordinate("E")]
        public bool HasOtherIncome { get; set; }

        [SpreadsheetHorizontalCoordinate("F")]
        public string LoanPurpose { get; set; }

        [SpreadsheetHorizontalCoordinate("G")]
        public double InterestRate { get; set; }

        [SpreadsheetHorizontalCoordinate("H")]
        public decimal Amount { get; set; }

        [SpreadsheetHorizontalCoordinate("I")]
        public DateTime LoanIssuedDate { get; set; }

        [SpreadsheetHorizontalCoordinate("J")]
        public int LoanOrder { get; set; }

        [SpreadsheetHorizontalCoordinate("K")]
        public bool Insured { get; set; }

        [SpreadsheetHorizontalCoordinate("L")]
        public string LoanStatus { get; set; }

        [SpreadsheetHorizontalCoordinate("M")]
        public decimal? RepaidEarly { get; set; }

        [SpreadsheetHorizontalCoordinate("N")]
        public int? PostponedRepayments { get; set; }

        [SpreadsheetHorizontalCoordinate("O")]
        public int CurrentDelay { get; set; }

        [SpreadsheetHorizontalCoordinate("P")]
        public decimal CurrentAmountDelayed { get; set; }

        [SpreadsheetHorizontalCoordinate("Q")]
        public DateTime? LastDeliquencyDate { get; set; }

        [SpreadsheetHorizontalCoordinate("R")]
        public int? MaximumDelay { get; set; }

        [SpreadsheetHorizontalCoordinate("S")]
        public int DelayedRepayments { get; set; }

        [SpreadsheetHorizontalCoordinate("T")]
        public decimal? MaximumDelayedAmount { get; set; }

        [SpreadsheetHorizontalCoordinate("U")]
        public bool Overpaid { get; set; }

        [SpreadsheetHorizontalCoordinate("V")]
        public int OriginalRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("W")]
        public int CurrentRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("X")]
        public int RemainingRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("Y")]
        public double? LostPercentage { get; set; }

        [SpreadsheetHorizontalCoordinate("Z")]
        public DateTime? LastRepayment { get; set; }

        [SpreadsheetHorizontalCoordinate("AA")]
        public decimal RepaidPrincipal { get; set; }

        [SpreadsheetHorizontalCoordinate("AB")]
        public decimal RepaidInterest { get; set; }

        [SpreadsheetHorizontalCoordinate("AC")]
        public decimal RepaidPenalty { get; set; }

        [SpreadsheetHorizontalCoordinate("AD")]
        public decimal RemainingPrincipal { get; set; }

        [SpreadsheetHorizontalCoordinate("AE")]
        public decimal RemainingInterest { get; set; }

        [SpreadsheetHorizontalCoordinate("AF")]
        public bool? LoanerIsInvestor { get; set; }

        [SpreadsheetHorizontalCoordinate("AG")]
        public bool HasStory { get; set; }

        [SpreadsheetHorizontalCoordinate("AH")]
        public int Investorcount { get; set; }
    }
}
