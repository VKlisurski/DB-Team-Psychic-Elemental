using CarDealersSystem.Data;
using MongoDBModule;
using System.Linq;

namespace CarDealersSystem.MongoDbSynchronizator
{
    public static class Synchronizator
    {
        public static void Run()
        {
            SyncCars();
            SyncDealers();
            SyncMakes();
            SyncModels();
            SyncTypes();
            SyncLoosesReports();
        }

        private static void SyncCars()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var cars = reader.GetCars();

                foreach (var currentCar in cars)
                {
                    var sqlCurrentCar = dbContext.Cars.Where(m => m.MakeName == currentCar.MakeName)
                        .Where(mm => mm.MakeName == currentCar.MakeName)
                        .FirstOrDefault();

                    if (sqlCurrentCar == null)
                    {
                        var newCar = new CarDealersSystem.Models.Car(currentCar.DealerName, currentCar.MakeName, currentCar.ModelName, currentCar.Price);
                        dbContext.Cars.Add(newCar);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static void SyncDealers()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var dealers = reader.GetDealers();

                foreach (var curDealer in dealers)
                {
                    var sqlCurrentDealer = dbContext.Dealers.Where(m => m.Name == curDealer.Name).FirstOrDefault();

                    if (sqlCurrentDealer == null)
                    { 
                        var newDealer = new CarDealersSystem.Models.Dealer(curDealer.Name, curDealer.Country, curDealer.City);
                        dbContext.Dealers.Add(newDealer);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static void SyncMakes()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var makes = reader.GetMakes();

                foreach (var curMake in makes)
                {
                    var sqlCurrentMake = dbContext.Makes.Where(m => m.Name == curMake.Name).FirstOrDefault();

                    if (sqlCurrentMake == null)
                    {
                        var newMake = new CarDealersSystem.Models.Make(curMake.Name);
                        dbContext.Makes.Add(newMake);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static void SyncModels()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var models = reader.GetModels();

                foreach (var curModel in models)
                {
                    var sqlCurrentMake = dbContext.Models.Where(m => m.Name == curModel.Name).FirstOrDefault();

                    if (sqlCurrentMake == null)
                    {
                        var newModel = new CarDealersSystem.Models.Model(curModel.Name, curModel.ManufacturingDate, curModel.TypeName);
                        dbContext.Models.Add(newModel);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static void SyncTypes()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var types = reader.GetAllTypes();

                foreach (var curType in types)
                {
                    var sqlCurrentType = dbContext.Types.Where(m => m.Name == curType.Name).FirstOrDefault();

                    if (sqlCurrentType == null)
                    {
                        var newDealer = new CarDealersSystem.Models.Type(curType.Name);
                        dbContext.Types.Add(newDealer);
                    }
                }

                dbContext.SaveChanges();
            }
        }

        private static void SyncLoosesReports()
        {
            using (var dbContext = new CarDealersSystemDbContext())
            {
                MongoDataReader reader = new MongoDataReader();
                var looses = reader.GetDealersLooses();

                foreach (var currLoose in looses)
                {
                    var sqlLoose = dbContext.LoosesReports.Where(l => l.ReportDate == currLoose.ReportDate).FirstOrDefault();

                    if (sqlLoose == null)
                    {
                        var newLoose = new CarDealersSystem.Models.LooseReport(currLoose.DealerName, currLoose.ReportDate, currLoose.Amount);
                        dbContext.LoosesReports.Add(newLoose);
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}
