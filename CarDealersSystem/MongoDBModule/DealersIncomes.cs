using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
    public class DealersIncomes
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public DateTime IncomeDate { get; set;}

        public decimal IncomeAmount { get; set; }

        [BsonConstructor]
        public DealersIncomes(string name, DateTime incomeDate, decimal incomeAmount)
        {
            this.Name = name;
            this.IncomeDate = incomeDate;
            this.IncomeAmount = incomeAmount;
        }

        public override string ToString()
        {
            return this.Name + " " + this.IncomeDate + " " + IncomeAmount;
        }
    }
}