namespace MySqlDbModule
{
    using System;

    public class TableBuilder
    {
        public void BuildAllTables()
        {
            if (!BuildTable("sales_reports"))
            {
                Console.WriteLine("Table sales_reports failed to build");
                return;
            }
            // add more tables here if some are needed
        }
    
        public bool BuildTable(string tableName)
        {
            string sql = string.Empty;

            switch (tableName)
            {
                case "sales_reports":
                    {
                        Console.WriteLine("Building table {0}", tableName);
                        DbBuilder dbb = new DbBuilder();
                        dbb.StartTable(tableName);
                        dbb.AddColumn(ColumnType.Int, "CarId", isNull: false, isUnique: true);
                        dbb.AddColumn(ColumnType.Varchar, "SalesReportFilePath", isNull: false, charLen: 50, isUnique: true);
                        dbb.AddColumn(ColumnType.Varchar, "CarModel", isNull: false, charLen: 50);
                        dbb.AddColumn(ColumnType.Varchar, "CarMake", isNull: false, charLen: 50);
                        dbb.AddColumn(ColumnType.Int, "QuantitySold", isNull: false);
                        dbb.AddColumn(ColumnType.Decimal, "TotalIncome", isNull: false);
                        sql = dbb.BuildSql();
                        break;
                    }
                default:
                    break;
            }
            
            MySqlDb.Exec(sql);

            return true;
        }
    }
}
