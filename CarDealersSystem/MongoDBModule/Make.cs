﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
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