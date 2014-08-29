namespace MongoDBModule
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Dealer
    {
        [BsonConstructor]
        public Dealer(string name, string country, string city, decimal income = 0)
        {
            this.Name = name;
            this.Country = country;
            this.City = city;
            this.Income = income;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public decimal Income { get; set; }

        public override string ToString()
        {
            return this.Name + " " + this.Country + " " + this.City + this.Income;
        }
    }
}
