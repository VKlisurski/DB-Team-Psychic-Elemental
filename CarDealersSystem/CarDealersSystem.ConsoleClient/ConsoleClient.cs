namespace CarDealersSystem.ConsoleClient
{
    using System;

    using CarDealersSystem.MongoDbSynchronizator;
    using CarDealersSystem.Reporters;
    using CarDealersSystem.Reporters.Contracts;

    using MySqlDbModule;
    
    using ZipExcelExtractor;

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
            var jsonReporter = new JsonReporter();
            jsonReporter.Report();

            var excelReporter = new ExcelReporter();
            excelReporter.Report();
        }
    }
}
