using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
    public class Dealer
    {
         [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public decimal Income { get; set; }

        [BsonConstructor]
        public Dealer(string name, string country, string city, decimal income = 0)
        {
            this.Name = name;
            this.Country = country;
            this.City = city;
            this.Income = income;
        }

        public override string ToString()
        {
            return this.Name + " " + this.Country + " " + this.City + this.Income;
        }
    }
}
