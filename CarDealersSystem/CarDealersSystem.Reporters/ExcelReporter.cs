namespace CarDealersSystem.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.OleDb;
    using System.Data.SQLite;

    using CarSalesSystem.Model;

    using MySql.Data.MySqlClient;

    using CarDealersSystem.Reporters;
    using CarDealersSystem.Reporters.Contracts;

    public class ExcelReporter : IReporter
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Report.xlsx;Extended Properties=Excel 12.0;";


        public void Report()
        {
            this.GetDataFromSqlite();
            this.WriteFromMySqlInExcel();
        }

        public void GetDataFromSqlite()
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=..\\..\\..\\..\\SQLiteDataBase\\ReportedBugs.db;Version=3;");
            sqliteConnection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT * FROM ReportedBugs", sqliteConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            var data = new List<object>();
            using (reader)
            {
                while (reader.Read())
                {
                    string carModel = (string)reader["CarModel"];
                    string description = (string)reader["Description"];
                    int importance = (int)reader["ImportanceLevel"];
                    data.Add(carModel);
                    data.Add(description);
                    data.Add(importance);
                    InsertInExcel(carModel, description, importance);
                }
            }

        }

        private static void InsertInExcel(string carModel, string description, int importance)
        {
            OleDbConnection excelConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Report.xlsx;Extended Properties=Excel 12.0;");
            excelConnection.Open();

            using (excelConnection)
            {
                OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Bugs$] (CarModel, Description, ImportanceLevel) VALUES (@CarModel, @Description, @ImportanceLevel)", excelConnection);
                insertCommand.Parameters.AddWithValue("@CarModel", carModel);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@ImportanceLevel", importance);

                insertCommand.ExecuteNonQuery();

                Console.WriteLine("Data inserted in Excel file successfully. Check out in bin\\debug.");
            }
        }

        private void WriteFromMySqlInExcel()
        {
            using (var context = new CarSalesSystemContext())
            {
                OleDbConnection con = new OleDbConnection(
                    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Report.xlsx;Extended Properties=Excel 12.0;");

                con.Open();

                var allSalesData = context.Sales_reports;

                OleDbCommand insertCommand = new OleDbCommand(
                    "INSERT INTO [Sales$] (CarModel, CarMake, QuantitySold, TotalIncome) VALUES (@CarModel, @CarMake, @QuantitySold, @TotalIncome)",
                    con);

                using (con)
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
                Console.WriteLine("Done.");
            }
        }
    }
}
