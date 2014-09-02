namespace CarDealersSystem.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Data.SQLite;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CarDealersSystem.Reporters;
    using CarDealersSystem.Reporters.Contracts;
    using CarSalesSystem.Model;

    using MySql.Data.MySqlClient;

    public class ExcelReporter : IReporter
    {
        private const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\..\\Excell Reports\\Report.xlsx;Extended Properties=Excel 12.0;";
        private const string SqliteConnectionString = "Data Source=..\\..\\..\\..\\SQLiteDataBase\\ReportedBugs.db;Version=3;";
        private const string SqliteToExcelTransferSuccessMessage = "Data transferred from SQLite in Excel file successfully. Check out in bin\\debug.";
        private const string MySqlToExcelTransferSuccessMessage = "Data transferred from MySql in Excel file successfully. Check out in bin\\debug.";

        public void Report()
        {
            this.GetDataFromSqlite();
            this.WriteFromMySqlInExcel();
        }

        public void GetDataFromSqlite()
        {
            var sqliteConnection = new SQLiteConnection(SqliteConnectionString);

            sqliteConnection.Open();

            var command = new SQLiteCommand("SELECT * FROM ReportedBugs", sqliteConnection);
            var reader = command.ExecuteReader();
            
            using (reader)
            {
                while (reader.Read())
                {
                    string carModel = (string)reader["CarModel"];
                    string description = (string)reader["Description"];
                    int importance = (int)reader["ImportanceLevel"];
                    
                    InsertInExcel(carModel, description, importance);
                }
            }
        }

        private static void InsertInExcel(string carModel, string description, int importance)
        {
            var excelConnection = new OleDbConnection(ExcelConnectionString);
            excelConnection.Open();

            using (excelConnection)
            {
                var insertCommand = new OleDbCommand("INSERT INTO [Bugs$] (CarModel, Description, ImportanceLevel) VALUES (@CarModel, @Description, @ImportanceLevel)", excelConnection);
                insertCommand.Parameters.AddWithValue("@CarModel", carModel);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@ImportanceLevel", importance);

                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine(SqliteToExcelTransferSuccessMessage);
        }

        private void WriteFromMySqlInExcel()
        {
            using (var context = new CarSalesSystemContext())
            {
                var connection = new OleDbConnection(ExcelConnectionString);

                connection.Open();

                var allSalesData = context.Sales_reports;

                var insertCommand = new OleDbCommand(
                    "INSERT INTO [Sales$] (CarModel, CarMake, QuantitySold, TotalIncome) VALUES (@CarModel, @CarMake, @QuantitySold, @TotalIncome)",
                    connection);

                using (connection)
                {
                    foreach (var item in allSalesData)
                    {
                        insertCommand.Parameters.AddWithValue("@CarModel", item.CarModel);
                        insertCommand.Parameters.AddWithValue("@CarMake", item.CarMake);
                        insertCommand.Parameters.AddWithValue("@QuantitySold", item.QuantitySold);
                        insertCommand.Parameters.AddWithValue("@TotalIncome", item.TotalIncome);

                        insertCommand.ExecuteNonQuery();
                    }
                }

                Console.WriteLine(MySqlToExcelTransferSuccessMessage);
            }
        }
    }
}
