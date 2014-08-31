namespace CarDealersSystem.ConsoleClient
{
    using System;

    using ZipExcelExtractor;

    using CarDealersSystem.MongoDbSynchronizator;
    using CarDealersSystem.Reporters.Contracts;
    using CarDealersSystem.Reporters;
    using MySqlDbModule;

    public class ConsoleClient
    {
        public static void Main()
        {           
            Synchronizator.Run();

            Extractor ext = new Extractor("..\\..\\");
            ext.ExtractFromArchive("Sales-Reports.zip");

            // Generates PDF report file
            PDFReporter.Export();
            Console.WriteLine("PDF Report generated successfully");

            // Creates MySQL database for the reports
            MySqlDbCreator.CreateDatabase();

            // Generates report files and sends the data for them to the MySQL database
            IReporter reporter = new JsonReporter();
            reporter.Report();

            var excelReporter = new ExcelReporter();
            excelReporter.Report();
        }
    }
}
