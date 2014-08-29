namespace CarDealersSystem.ConsoleClient
{
    using System;

    using ZipExcelExtractor;

    using CarDealersSystem.MongoDbSynchronizator;
    using CarDealersSystem.Reporters.Contracts;
    using CarDealersSystem.Reporters;

    public class ConsoleClient
    {
        public static void Main()
        {
            Synchronizator.Run();
            
            Extractor ext = new Extractor("..\\..\\");
            ext.ExtractFromArchive("Sales-Reports.zip");

            IReporter reporter = new JsonReporter();
            reporter.Report();
        }
    }
}
