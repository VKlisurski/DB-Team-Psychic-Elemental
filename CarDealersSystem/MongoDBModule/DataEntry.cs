namespace MongoDBModule
{
    using System;

    internal static class DataEntry
    {
        internal static void FillWithData()
        {
            var mongoInserter = new MongoDataInserter();

            // Cars
            for (int i = 0; i < 10; i++)
            {
                var newCar = new Car("Overdrive", "Opel", "Astra", (decimal)32000);
                mongoInserter.AddCar(newCar);
            }

            // Dealers
            for (int i = 0; i < 10; i++)
            {
                string dealerName = "DealerName" + i;
                string countryName = "Country" + i;
                string cityName = "City" + i;
                var newDealer = new Dealer(dealerName, countryName, cityName);
                mongoInserter.AddDealer(newDealer);
            }

            // Make
            for (int i = 0; i < 10; i++)
            {
                string makeName = "Make" + i;
                var newMake = new Make(makeName);
                mongoInserter.AddMake(newMake);
            }

            // Models
            for (int i = 0; i < 10; i++)
            {
                string modelName = "ModelName" + i;
                DateTime modelManufacturingDate = DateTime.Now.AddYears(-i);
                var newModel = new Model(modelName, modelManufacturingDate, "Coupe");
                mongoInserter.AddModel(newModel);
            }

            // Types
            string[] types = new string[] { "Coupe", "Sedan", "Compy", "Cabrio", "Van" };
            for (int i = 0; i < types.Length; i++)
            {
                var newType = new Type(types[i]);
                mongoInserter.AddType(newType);
            }
        }
    }
}