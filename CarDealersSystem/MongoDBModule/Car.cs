namespace MongoDBModule
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Car
    {
        [BsonConstructor]
        public Car(string dealerName, string makeName, string modelName, decimal price)
        {
            this.DealerName = dealerName;
            this.MakeName = makeName;
            this.ModelName = modelName;
            this.Price = price;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string DealerName { get; set; }

        public string MakeName { get; set; }

        public string ModelName { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format("ModelId: {0} Make: {1} Dealer: {2} Price{3}", this.ModelName, this.MakeName, this.DealerName, this.Price);
        }
    }
}