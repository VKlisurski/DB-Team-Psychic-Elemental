using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
    public class LooseReport
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string DealerName { get; set; }

        public DateTime ReportDate { get; set;}

        public decimal Amount { get; set; }

        [BsonConstructor]
        public LooseReport(string dealerName, DateTime reportDate, decimal amount)
        {
            this.DealerName = dealerName;
            this.ReportDate = reportDate;
            this.Amount = amount;
        }

        public override string ToString()
        {
            return this.DealerName + " " + this.ReportDate + " " + Amount + " " + this.ReportDate;
        }
    }
}