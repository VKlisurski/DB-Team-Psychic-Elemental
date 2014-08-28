using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
    class Program
    {
        static void Main(string[] args)
        {
            // NOTE Some or All of this will be readed from XML. This is temporary solution

            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var carsDb = mongoServer.GetDatabase("CarDealersSystem");
            var carsCollection = carsDb.GetCollection("Cars");

            //carsDb.CreateCollection("Cars"); //uncomment first run to create collections
            //carsDb.CreateCollection("Dealers");
            //carsDb.CreateCollection("Models");
            //carsDb.CreateCollection("Makes");
            //carsDb.CreateCollection("Types");

            //Database.FillWithData(mongoClient); // uncomment first run to fill data

            var modelsCollection = carsDb.GetCollection("Models");
            var models = modelsCollection.FindAllAs<Model>();

            foreach (var model in models)
            {
                Console.WriteLine(model);
            }
        }
    }
}
