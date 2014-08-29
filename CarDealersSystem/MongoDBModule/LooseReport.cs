namespace MongoDBModule
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class LooseReport
    {
        [BsonConstructor]
        public LooseReport(string dealerName, DateTime reportDate, decimal amount)
        {
            this.DealerName = dealerName;
            this.ReportDate = reportDate;
            this.Amount = amount;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string DealerName { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal Amount { get; set; }

        public override string ToString()
        {
            return this.DealerName + " " + this.ReportDate + " " + this.Amount + " " + this.ReportDate;
        }
    }
}