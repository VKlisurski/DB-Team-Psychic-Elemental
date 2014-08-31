namespace CarDealersSystem.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    using CarDealersSystem.Data;
    using CarDealersSystem.Reporters.Contracts;
    using CarDealersSystem.Reporters.ReportModels;

    public class JsonReporter : IReporter
    {
        private const string ConnectionString = @"Server=localhost;Port=3306;Database=sales_reports_database;Uid=root;Pwd=12345;";
        private const string ReportsPath = "..\\..\\..\\..\\Sales reports\\";
        private const string ReportsFileExtension = ".json";

        public void Report()
        {
            Console.WriteLine("Started creating JSON reports...");

            if (!Directory.Exists(ReportsPath))
            {
                Console.WriteLine("Creating Sales reports folder...");
                Directory.CreateDirectory(ReportsPath);
            }

            var dbContext = new CarDealersSystemDbContext();

            var reports = JsonReporter.GetJsonReports(dbContext);

            var saveFilesPaths = JsonReporter.SaveReportsToFiles(reports);

            JsonReporter.SendReportToDatabase(reports, saveFilesPaths);

            Console.WriteLine("JSON reports created.");
        }

        private static IQueryable<JsonReport> GetJsonReports(CarDealersSystemDbContext dbContext)
        {
            Console.WriteLine("Generating reports from the SQL Server database data...");

            var cars = dbContext.Cars;
            var sales = dbContext.SalesReports;

            var reports = cars.Join(sales, car => car.CarID, sale => sale.CarID,
                                       (car, sale) => new JsonReport
                                       {
                                           CarId = car.CarID,
                                           CarModer = car.ModelName,
                                           CarMake = car.MakeName,
                                           PricePerUnit = car.Price,
                                           Quantity = sales.Where(rep => rep.CarID == car.CarID)
                                               .Sum(profit => profit.Quantity),
                                           Sum = sales.Where(rep => rep.CarID == car.CarID)
                                               .Sum(profit => profit.Sum)
                                       });

            return reports;
        }

        private static List<string> SaveReportsToFiles(IQueryable<JsonReport> reports)
        {
            Console.WriteLine("Saving reports to files...");

            List<string> reportsFilePaths = new List<string>();

            foreach (var report in reports)
            {
                string reportFilePath = string.Format("{0}{1}{2}", ReportsPath, report.CarId, ReportsFileExtension);

                StreamWriter writer = new StreamWriter(reportFilePath);

                using (writer)
                {
                    string serialized = JsonConvert.SerializeObject(report, Formatting.Indented);

                    writer.WriteLine(serialized);

                    reportsFilePaths.Add(reportFilePath);
                }
            }

            return reportsFilePaths;
        }

        private static void SendReportToDatabase(IQueryable<JsonReport> reportsQuery, List<string> saveFilesPaths)
        {
            Console.WriteLine("Sending report data to MySQL database...");

            var reports = reportsQuery.ToList<JsonReport>();

            for (int i = 0, length = reports.Count; i < length; i++)
            {
                JsonReport report = reports[i];
                string reportFilePath = saveFilesPaths[i];

                MySqlConnection connection = new MySqlConnection(ConnectionString);

                using (connection)
                {
                    connection.Open();

                    string commandText = JsonReporter.GetCommandText(connection, reportFilePath);

                    MySqlCommand command = new MySqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@id", i + 1);
                    command.Parameters.AddWithValue("@carId", report.CarId);
                    command.Parameters.AddWithValue("@filePath", reportFilePath);
                    command.Parameters.AddWithValue("@carModel", report.CarModer);
                    command.Parameters.AddWithValue("@carMake", report.CarMake);
                    command.Parameters.AddWithValue("@quantity", report.Quantity);
                    command.Parameters.AddWithValue("@totalIncome", report.Sum);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static string GetCommandText(MySqlConnection connection, string reportFilePath)
        {
            string returnCommand = "INSERT INTO sales_reports(Id, CarId, SalesReportFilePath, CarModel, CarMake, QuantitySold, TotalIncome) " +
                                   "VALUES (@id, @carId, @filePath, @carModel, @carMake, @quantity, @totalIncome);";

            MySqlCommand reportFileCheckCommand = new MySqlCommand("SELECT SalesReportFilePath FROM sales_reports", connection);
            var reader = reportFileCheckCommand.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string rowValue = (string)reader["SalesReportFilePath"];

                    if (rowValue == reportFilePath)
                    {
                        returnCommand = "UPDATE sales_reports " +
                                        "SET QuantitySold = @quantity, TotalIncome = @totalIncome " +
                                        "WHERE SalesReportFilePath = @filePath;";
                        break;
                    }
                }
            }

            return returnCommand;
        }
    }
}
