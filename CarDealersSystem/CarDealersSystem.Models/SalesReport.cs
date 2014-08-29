using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealersSystem.Models
{
    public class SalesReport
    {
        public SalesReport() { }

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

        public DateTime ReportDate { get; set;}

        public int Quantity { get; set; }

        public decimal Sum { get; set; }

        public override string ToString()
        {
            return this.DealerName + this.CarID + " " + this.ReportDate + " " + this.Quantity + " " + Sum;
        }
    }
}
