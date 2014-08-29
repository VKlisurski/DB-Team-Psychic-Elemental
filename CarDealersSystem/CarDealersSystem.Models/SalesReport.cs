namespace CarDealersSystem.Models
{
    using System;

    public class SalesReport
    {
        public SalesReport()
        {
        }

        public SalesReport(string dealerName, int carID, DateTime reportDate, int quantity, decimal sum)
        {
            this.DealerName = dealerName;
            this.CarID = carID;
            this.ReportDate = reportDate;
            this.Quantity = quantity;
            this.Sum = sum;
        }

        public int SalesReportID { get; set; }

        public string DealerName { get; set; }

        public int CarID { get; set; }

        public DateTime ReportDate { get; set; }

        public int Quantity { get; set; }

        public decimal Sum { get; set; }

        public override string ToString()
        {
            return this.DealerName + this.CarID + " " + this.ReportDate + " " + this.Quantity + " " + this.Sum;
        }
    }
}
