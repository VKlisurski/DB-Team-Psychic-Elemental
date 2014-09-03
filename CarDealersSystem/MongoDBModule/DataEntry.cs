namespace MongoDBModule
{
    using System;

    internal static class DataEntry
    {
        internal static void FillWithData()
        {
            var mongoInserter = new MongoDataInserter();

            // Cars
            var opelAstra = new Car("Mobile", "Opel", "Astra", (decimal)15000);
            var opelCorsa = new Car("Gurublqne", "Opel", "Corsa", (decimal)4500);
            var vwGolf = new Car("Gurublqne", "Vw", "Golf4", (decimal)4000);
            var vwPassat = new Car("Mobile", "Vw", "Passat", (decimal)12000);
            var audiS7 = new Car("Overdrive", "Audi", "S7", (decimal)45000);
            var audiA4 = new Car("Overdrive", "Audi", "A4", (decimal)24000);
            var pegeuot206 = new Car("Mobile", "Pegeout", "206", (decimal)3500);
            var audi80 = new Car("Mobile", "Audi", "80", (decimal)2500);
            var toyotaCorrola = new Car("Gurublqne", "Toyota", "Corolla", (decimal)8000);
            var pegeout407 = new Car("Mobile", "Pegeout", "407", (decimal)9000);
            var opelVectra = new Car("Mobile", "Opel", "Vectra", (decimal)17000);
            var bwm3 = new Car("Overdrive", "Bmw", "3", (decimal)32000);

            mongoInserter.AddCar(opelAstra);
            mongoInserter.AddCar(opelCorsa);
            mongoInserter.AddCar(vwGolf);
            mongoInserter.AddCar(vwPassat);
            mongoInserter.AddCar(audiS7);
            mongoInserter.AddCar(audiA4);
            mongoInserter.AddCar(pegeuot206);
            mongoInserter.AddCar(audi80);
            mongoInserter.AddCar(toyotaCorrola);
            mongoInserter.AddCar(pegeout407);
            mongoInserter.AddCar(opelVectra);
            mongoInserter.AddCar(bwm3);


            // Dealers
            var ovedrive = new Dealer("Overdrive", "Bulgaria", "Sofia", 0);
            var mobile = new Dealer("Mobile", "Bulgaria", "Sofia", 0);
            var gurublqne = new Dealer("Gurublqne", "Bulgaria", "Sofia", 0);

            mongoInserter.AddDealer(ovedrive);
            mongoInserter.AddDealer(mobile);
            mongoInserter.AddDealer(gurublqne);

            // Make
            var opelMake = new Make("Opel");
            var vwMake = new Make("Vw");
            var audiMake = new Make("Audi");
            var pegeoutMake = new Make("Pegeout");
            var toyotaMake = new Make("Toyota");
            var bmwMake = new Make("Bmw");

            mongoInserter.AddMake(opelMake);
            mongoInserter.AddMake(vwMake);
            mongoInserter.AddMake(audiMake);
            mongoInserter.AddMake(pegeoutMake);
            mongoInserter.AddMake(toyotaMake);
            mongoInserter.AddMake(bmwMake);

            // Models
            var astraModel = new Model("Astra", DateTime.Now.AddYears(-10), "Coupe");
            var corsaModel = new Model("Corsa", DateTime.Now.AddYears(-5), "Sedan");
            var golf4Model = new Model("Golf4", DateTime.Now.AddYears(-15), "Sport");
            var passatModel = new Model("Passat", DateTime.Now.AddYears(-8), "Comby");
            var s7Model = new Model("S7", DateTime.Now.AddYears(-2), "Sport");
            var a4Model = new Model("A4", DateTime.Now.AddYears(-4), "Coupe");
            var model206 = new Model("206", DateTime.Now.AddYears(-10), "Cabrio");
            var model80 = new Model("80", DateTime.Now.AddYears(-20), "Comby");
            var corollaModel = new Model("Corolla", DateTime.Now.AddYears(-5), "Coupe");
            var model407 = new Model("407", DateTime.Now.AddYears(-12), "Coupe");
            var vectraModel = new Model("Vectra", DateTime.Now.AddYears(-6), "Coupe");
            var model3Bmw = new Model("3", DateTime.Now.AddYears(-10), "Cabrio");

            mongoInserter.AddModel(astraModel);
            mongoInserter.AddModel(corsaModel);
            mongoInserter.AddModel(golf4Model);
            mongoInserter.AddModel(passatModel);
            mongoInserter.AddModel(s7Model);
            mongoInserter.AddModel(a4Model);
            mongoInserter.AddModel(model206);
            mongoInserter.AddModel(model80);
            mongoInserter.AddModel(corollaModel);
            mongoInserter.AddModel(model407);
            mongoInserter.AddModel(vectraModel);
            mongoInserter.AddModel(model3Bmw);

            // Types
            string[] types = new string[] { "Coupe", "Sedan", "Comby", "Cabrio", "Van", "Sport" };
            for (int i = 0; i < types.Length; i++)
            {
                var newType = new Type(types[i]);
                mongoInserter.AddType(newType);
            }
        }
    }
}