using System;
using XmlController;

namespace MongoDBModule
{
    public static class Importer
    {
        public static void ImportLoosesReportsFromXml()
        {
            MongoDataInserter inserter = new MongoDataInserter();
            var allLooses = ReadXml.GetObjects("..\\..\\..\\Looses-Reports.xml");

            foreach (var loose in allLooses)
            {
                var dealerName = loose.Item1.ToString();
                DateTime reportDate = DateTime.Parse(loose.Item2);
                decimal amount = decimal.Parse(loose.Item3);

                var currentLoose = new LooseReport(dealerName, reportDate, amount);
                inserter.AddLooseReport(currentLoose);
            }
        }
    }
}