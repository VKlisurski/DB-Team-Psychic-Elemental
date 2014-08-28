using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBModule
{
    internal static class Database
    {
        internal static void CreateCollection(MongoClient client, string dbName, string collectionName)
        {
            var mongoServer = client.GetServer();
            var database = mongoServer.GetDatabase(dbName);
            database.CreateCollection(collectionName);
        }

        internal static void FillWithData(MongoClient client)
        {
            var mongoServer = client.GetServer();
            var carsDb = mongoServer.GetDatabase("CarDealersSystem");

            // Cars
            var carsCollection = carsDb.GetCollection("Cars");

            for (int i = 0; i < 10; i++)
            {
                var newCar = new Car("Overdrive", "Opel", "Astra", (decimal)32000);
                carsCollection.Insert(newCar);
            }

            // Dealers
            var dealersCollection = carsDb.GetCollection("Dealers");
            for (int i = 0; i < 10; i++)
            {
                string dealerName = "DealerName" + i;
                string countryName = "Country" + i;
                string cityName = "City" + i;
                var newDealer = new Dealer(dealerName, countryName, cityName);
                dealersCollection.Insert(newDealer);
            }

            // Make
            var makeCollection = carsDb.GetCollection("Makes");
            for (int i = 0; i < 10; i++)
            {
                string makeName = "Make" + i;
                var newMake = new Make(makeName);
                makeCollection.Insert(newMake);
            }

            // Models
            var modelsCollection = carsDb.GetCollection("Models");
            for (int i = 0; i < 10; i++)
            {
                string modelName = "ModelName" + i;
                DateTime modelManufacturingDate = DateTime.Now.AddYears(-i);
                var newModel = new Model(modelName, modelManufacturingDate, "Coupe");
                modelsCollection.Insert(newModel);
            }

            // Types
            var typesCollection = carsDb.GetCollection("Types");
            string[] types = new string[] { "Coupe", "Sedan", "Compy", "Cabrio", "Van" };
            for (int i = 0; i < types.Length; i++)
            {
                var typeCoupe = new Type(types[i]);
                typesCollection.Insert(typeCoupe);
            }
        }
    }
}