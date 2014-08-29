namespace MongoDBModule
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Make
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        [BsonConstructor]
        public Make(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
