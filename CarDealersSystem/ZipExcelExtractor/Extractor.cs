namespace ZipExcelExtractor
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.IO.Compression;

    using CarDealersSystem.Data;
    using CarDealersSystem.Models;

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
            if (Directory.Exists(this.pathToArchive + FileForExtract))
            {
                Directory.Delete(this.pathToArchive + FileForExtract, true);
            }

            ZipFile.ExtractToDirectory(this.pathToArchive + archiveName, this.pathToArchive + FileForExtract);
            var allFolders = Directory.GetDirectories(this.pathToArchive + FileForExtract);

            foreach (var folder in allFolders)
            {
                var folderName = Path.GetFileName(folder);
                var allFiles = Directory.GetFiles(folder);

                foreach (var file in allFiles)
                {
                    this.ExcelParser(folderName, file);
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
                    this.InsertToSQL(dealerName, carID, date, quantity, sum);
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
