namespace MongoDBModule
{
    using MongoDB.Driver;

    public class EntryPoint
    {
        public static void Main()
        {
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var carsDb = mongoServer.GetDatabase("CarDealersSystem");
            var carsCollection = carsDb.GetCollection("Cars");

            //carsDb.CreateCollection("Cars");
            //carsDb.CreateCollection("Dealers");
            //carsDb.CreateCollection("Models");
            //carsDb.CreateCollection("Makes");
            //carsDb.CreateCollection("Types");

            //DataEntry.FillWithData();

            Importer.ImportLoosesReportsFromXml();
        }
    }
}
