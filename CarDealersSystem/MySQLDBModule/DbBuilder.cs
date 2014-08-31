namespace MySqlDbModule
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DbBuilder
    {
        private static readonly string crlf = Environment.NewLine;

        private string tablename;

        private List<DbColumn> columns;

        public DbBuilder()
        {
        }

        public void StartTable(string tableName)
        {
            this.tablename = tableName;
            this.columns = new List<DbColumn>();
        }

        public string BuildSql()
        {
            string primary = string.Empty;
            string unique = string.Empty;

            if (columns == null)
            {
                throw new Exception("Mising StartTable in class DbBuilder");
            }

            StringBuilder sql = new StringBuilder("CREATE TABLE IF NOT EXISTS `sales_reports_database`.")
                .Append(string.Format("`{0}`", tablename))
                .Append("(")
                .Append(crlf);

            for (int colIndex = 0; colIndex < columns.Count; colIndex++)
            {
                DbColumn dbcol = columns[colIndex];
                sql.Append(string.Format("`{0}`", dbcol.ColName));
                sql.Append(dbcol.ColTypeString)
                    .Append(',')
                    .Append(crlf);

                if (primary == string.Empty && dbcol.IsPrimary)
                {
                    primary = string.Format("CONSTRAINT `PK_{0}` PRIMARY KEY (`{0}`),", dbcol.ColName);
                }

                if (unique == string.Empty && dbcol.IsUnique)
                {
                    unique = string.Format("UNIQUE INDEX `{0}_UNIQUE` (`{0}` ASC)", dbcol.ColName);
                }
                else if (dbcol.IsUnique)
                {
                    unique += string.Format(",{0}UNIQUE INDEX `{1}_UNIQUE` (`{1}` ASC)", crlf, dbcol.ColName);
                }
            }

            if (primary != string.Empty)
            {
                sql.Append(primary)
                    .Append(crlf);
            }

            if (unique != string.Empty)
            {
                sql.Append(unique)
                    .Append(crlf);
            }

            sql.Append(")")
                .Append(crlf)
                .Append("ENGINE = InnoDB")
                .Append(crlf)
                .Append("DEFAULT CHARACTER SET = latin1;")
                .Append(crlf);

            return sql.ToString();
        }

        public void AddColumn(ColumnType type, string columnName, bool isIndex = false, bool isPrimary = false, int charLen = 0, bool isNull = true, bool isUnique = false)
        {
            DbColumn dbc = new DbColumn(type, columnName, isIndex, isPrimary, charLen, isNull, isUnique);
            columns.Add(dbc);
        }
    }
}
