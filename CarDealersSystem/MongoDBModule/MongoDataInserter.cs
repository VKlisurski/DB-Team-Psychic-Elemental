namespace MongoDBModule
{
    using MongoDB.Driver;

    public class MongoDataInserter
    {
        private MongoDatabase database;

        public MongoDataInserter()
        {
            var connection = new MongoClient("mongodb://localhost/");
            var server = connection.GetServer();
            this.database = server.GetDatabase("CarDealersSystem");
        }

        public void AddCar(Car car)
        {
            var companies = this.database.GetCollection("Cars");
            companies.Insert(car);
        }

        public void AddDealer(Dealer dealer)
        {
            var customers = this.database.GetCollection("Dealers");
            customers.Insert(dealer);
        }

        public void AddModel(Model model)
        {
            var destinations = this.database.GetCollection("Models");
            destinations.Insert(model);
        }

        public void AddMake(Make make)
        {
            var destinations = this.database.GetCollection("Makes");
            destinations.Insert(make);
        }

        public void AddType(Type type)
        {
            var destinations = this.database.GetCollection("Types");
            destinations.Insert(type);
        }

        public void AddLooseReport(LooseReport loose)
        {
            var destination = this.database.GetCollection("LoosesReports");
            destination.Insert(loose);
        }
    }
}
