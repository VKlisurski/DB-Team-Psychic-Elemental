namespace MongoDBModule
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Type
    {
        [BsonConstructor]
        public Type(string name)
        {
            this.Name = name;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
