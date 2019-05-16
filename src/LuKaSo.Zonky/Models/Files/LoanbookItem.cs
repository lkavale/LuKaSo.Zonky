using LuKaSo.Zonky.Attributes;
using System;

namespace LuKaSo.Zonky.Models.Files
{
    [SpreadsheetSheet("data")]
    [SpreadsheetVerticalCoordinate(2)]
    public class LoanbookItem
    {
        [SpreadsheetHorizontalCoordinate("A")]
        public string Id { get; set; }

        [SpreadsheetHorizontalCoordinate("B")]
        public string Region { get; set; }

        [SpreadsheetHorizontalCoordinate("C")]
        public string MainIncomeType { get; set; }

        [SpreadsheetHorizontalCoordinate("D")]
        public string LoanPurpose { get; set; }

        [SpreadsheetHorizontalCoordinate("E")]
        public double InterestRate { get; set; }

        [SpreadsheetHorizontalCoordinate("F")]
        public decimal Amount { get; set; }

        [SpreadsheetHorizontalCoordinate("G")]
        public DateTime LoanIssuedDate { get; set; }

        [SpreadsheetHorizontalCoordinate("H")]
        public int LoanOrder { get; set; }

        [SpreadsheetHorizontalCoordinate("I")]
        public bool Insured { get; set; }

        [SpreadsheetHorizontalCoordinate("J")]
        public string LoanStatus { get; set; }

        [SpreadsheetHorizontalCoordinate("K")]
        public decimal? RepaidEarly { get; set; }

        [SpreadsheetHorizontalCoordinate("L")]
        public int? PostponedRepayments { get; set; }

        [SpreadsheetHorizontalCoordinate("M")]
        public int CurrentDelay { get; set; }

        [SpreadsheetHorizontalCoordinate("N")]
        public decimal CurrentAmountDelayed { get; set; }

        [SpreadsheetHorizontalCoordinate("O")]
        public DateTime? LastDelinquencyDate { get; set; }

        [SpreadsheetHorizontalCoordinate("P")]
        public int? MaximumDelay { get; set; }

        [SpreadsheetHorizontalCoordinate("Q")]
        public int DelayedRepayments { get; set; }

        [SpreadsheetHorizontalCoordinate("R")]
        public decimal? MaximumDelayedAmount { get; set; }

        [SpreadsheetHorizontalCoordinate("S")]
        public bool Overpaid { get; set; }

        [SpreadsheetHorizontalCoordinate("T")]
        public int OriginalRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("U")]
        public int CurrentRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("V")]
        public int RemainingRepaymentCount { get; set; }

        [SpreadsheetHorizontalCoordinate("W")]
        public double LostPercentage { get; set; }

        [SpreadsheetHorizontalCoordinate("X")]
        public DateTime? LastRepayment { get; set; }

        [SpreadsheetHorizontalCoordinate("Y")]
        public decimal RepaidPrincipal { get; set; }

        [SpreadsheetHorizontalCoordinate("Z")]
        public decimal RepaidInterest { get; set; }

        [SpreadsheetHorizontalCoordinate("AA")]
        public decimal RepaidPenalty { get; set; }

        [SpreadsheetHorizontalCoordinate("AB")]
        public decimal RemainingPrincipal { get; set; }

        [SpreadsheetHorizontalCoordinate("AC")]
        public decimal RemainingInterest { get; set; }

        [SpreadsheetHorizontalCoordinate("AD")]
        public bool? LoanerIsInvestor { get; set; }

        [SpreadsheetHorizontalCoordinate("AE")]
        public bool HasStory { get; set; }

        [SpreadsheetHorizontalCoordinate("AF")]
        public int Investorcount { get; set; }
    }
}
