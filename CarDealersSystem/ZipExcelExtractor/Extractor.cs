using CarDealersSystem.Data;
using CarDealersSystem.Models;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
namespace ZipExcelExtractor
{
    public class Extractor
    {
        private const string FileForExtract = "Reports";
        private string pathToArchive;

        public Extractor(string pathToArchive)
        {
            this.pathToArchive = pathToArchive;
        }

        public void ExtractFromArchive(string archiveName)
        {
            if (Directory.Exists(pathToArchive + FileForExtract))
            {
                Directory.Delete(pathToArchive + FileForExtract, true);
            }
            ZipFile.ExtractToDirectory(pathToArchive + archiveName, pathToArchive + FileForExtract);
            var allFolders = Directory.GetDirectories(pathToArchive + FileForExtract);
            foreach (var folder in allFolders)
            {
                var folderName = Path.GetFileName(folder);
                var allFiles = Directory.GetFiles(folder);
                foreach (var file in allFiles)
                {
                    ExcelParser(folderName, file);
                }
            }
        }

        private void ExcelParser(string folderName, string pathOfFile)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", pathOfFile);
            OleDbConnection con = new OleDbConnection(connectionString);
            con.Open();
            using (con)
            {
                var dataTable = new DataTable();
                var adapter = new OleDbDataAdapter("select * from [SalesReports$] ", con);
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    string dealerName = row.ItemArray[0].ToString();
                    int carID = int.Parse(row.ItemArray[1].ToString());
                    int quantity = int.Parse(row.ItemArray[2].ToString());
                    decimal sum = decimal.Parse(row.ItemArray[3].ToString());
                    DateTime date = DateTime.ParseExact(folderName, "dd-mm-yyyy", null);
                    InsertToSQL(dealerName, carID, date, quantity, sum);
                }
            }
        }

        private void InsertToSQL(string dealerName, int carID, DateTime date, int quantity, decimal sum)
        {
            using (var db = new CarDealersSystemDbContext())
            {
                var newSalesReport = new SalesReport(dealerName, carID, date, quantity, sum);
                db.SalesReports.Add(newSalesReport);
                db.SaveChanges();
            }
        }
    }
}
