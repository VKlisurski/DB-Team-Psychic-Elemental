namespace MongoDBModule
{
    using MongoDB.Driver;

    public class Program
    {
        public static void Main()
        {
            // NOTE: All the data entry will be fixed. This is temporary solution.
            // NOTE2: The DealersIncomes will be parsed from XML

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
