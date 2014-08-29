namespace CarDealersSystem.Reporters
{
    using System;
    using System.IO;
    using System.Linq;

    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    using CarDealersSystem.Data;
    using CarDealersSystem.Reporters.Contracts;
    using CarDealersSystem.Reporters.ReportModels;

    public class JsonReporter : IReporter
    {
        private const string ReportsPath = "..\\..\\..\\..\\Sales reports\\";
        private const string ConnectionString = @"Server=localhost;Port=3306;Database=sales_reports_database;Uid=root;Pwd=12345;";

        public void Report()
        {
            if (!Directory.Exists(ReportsPath))
            {
                Directory.CreateDirectory(ReportsPath);
            }

            var dbContext = new CarDealersSystemDbContext();

            var reports = dbContext.Cars
                .Join(dbContext.SalesReports, sale => sale.CarID, car => car.CarID,
                    (car, sale) => new JsonReport
                        {
                            Id = car.CarID,
                            CarModer = car.ModelName,
                            CarMake = car.MakeName,
                            PricePerUnit = car.Price,
                            Quantity = dbContext.SalesReports
                                .Where(rep => rep.CarID == car.CarID)
                                .Sum(profit => profit.Quantity),
                            Sum = dbContext.SalesReports
                                .Where(rep => rep.CarID == car.CarID)
                                .Sum(profit => profit.Sum)
                        });

            foreach (var report in reports)
            {
                string serialized = JsonConvert.SerializeObject(report, Formatting.Indented);

                string reportFilePath = string.Format("{0}{1}.json", ReportsPath, report.Id);

                using (StreamWriter writer = new StreamWriter(reportFilePath))
                {
                    writer.WriteLine(serialized);
                    //this.InsertReportIntoDatabase(report, reportFilePath);
                }
            }
        }

        private void InsertReportIntoDatabase(JsonReport report, string reportFilePath)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);

            string commandText = "INSERT INTO sales_reports(car-id, sales-report-file-path, car-model, car-make, quantity-sold, total-income) " +
                    "VALUES (@carId, @filePath, @carModel, @carMake, @quantity, @totalIncome);";

            MySqlCommand command = new MySqlCommand(commandText, connection);
            using (connection)
            {
                connection.Open();

                command.Parameters.AddWithValue("@carId", report.Id);
                command.Parameters.AddWithValue("@filePath", reportFilePath);
                command.Parameters.AddWithValue("@carModel", report.CarModer);
                command.Parameters.AddWithValue("@carMake", report.CarMake);
                command.Parameters.AddWithValue("@quantity", report.Quantity);
                command.Parameters.AddWithValue("@totalIncome", report.Sum);

                command.ExecuteNonQuery();
            }
        }
    }
}
