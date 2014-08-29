namespace MongoDBModule
{
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    public class MongoDataReader
    {
        private MongoDatabase database;

        public MongoDataReader()
        {
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            this.database = mongoServer.GetDatabase("CarDealersSystem");
        }

        public IEnumerable<Car> GetCars()
        {
            var carsCollection = this.database.GetCollection("Cars");
            var allCars = carsCollection.FindAllAs<Car>();
            var list = allCars.ToList();

            return list;
        }

        public IEnumerable<Dealer> GetDealers()
        {
            var dealersCollection = this.database.GetCollection("Dealers");
            var allDealers = dealersCollection.FindAllAs<Dealer>();
            var list = allDealers.ToList();

            return list;
        }

        public IEnumerable<Make> GetMakes()
        {
            var makesCollection = this.database.GetCollection("Makes");
            var allMakes = makesCollection.FindAllAs<Make>();
            var list = allMakes.ToList();

            return list;
        }

        public IEnumerable<Model> GetModels()
        {
            var modelsCollection = this.database.GetCollection("Models");
            var allModels = modelsCollection.FindAllAs<Model>();
            var list = allModels.ToList();

            return list;
        }

        public IEnumerable<Type> GetAllTypes()
        {
            var typesCollection = this.database.GetCollection("Types");
            var allTypes = typesCollection.FindAllAs<Type>();
            var list = allTypes.ToList();

            return list;
        }

        public IEnumerable<LooseReport> GetDealersLooses()
        {
            var typesCollection = this.database.GetCollection("LoosesReports");
            var allDealers = typesCollection.FindAllAs<LooseReport>();
            var list = allDealers.ToList();

            return list;
        }
    }
}
