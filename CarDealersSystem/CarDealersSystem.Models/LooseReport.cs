namespace CarDealersSystem.Models
{
    using System;

    public class LooseReport
    {
        public LooseReport()
        {
        }

        public LooseReport(string dealerName, DateTime reportDate, decimal amount)
        {
            this.DealerName = dealerName;
            this.ReportDate = reportDate;
            this.Amount = amount;
        }

        public int LooseReportID { get; set; }

        public string DealerName { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal Amount { get; set; }
    }
}
