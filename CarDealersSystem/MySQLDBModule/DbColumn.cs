namespace MySqlDbModule
{
    public class DbColumn
    {
        private bool isNull;

        public DbColumn(ColumnType type, string columnName, bool isIndex = false, bool isPrimary = false, int charlen = 0, bool isnull = true, bool isUnique = false)
        {
            this.ColType = type;
            this.ColName = columnName;
            this.IsIndex = isIndex;
            this.CharLen = charlen;
            this.isNull = isnull;
            this.IsPrimary = isPrimary;
            this.IsUnique = isUnique;
        }
        
        public string ColName { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsUnique { get; set; }

        private ColumnType ColType { get; set; }

        private int CharLen { get; set; }
        
        private bool IsIndex { get; set; }

        private string IsNull
        {
            get
            {
                return this.isNull ? "NULL " : "NOT NULL";
            }
        }

        public string ColTypeString
        {
            get
            {
                switch (this.ColType)
                {
                    case ColumnType.Int:
                        return string.Format(" INT(11) {0}", this.IsNull);
                    case ColumnType.Varchar:
                        return string.Format(" VARCHAR({0}) {1}", this.CharLen, this.IsNull);
                    case ColumnType.DateTime:
                        return string.Format(" DATETIME {0}", this.IsNull);
                    case ColumnType.Decimal:
                        return string.Format(" DECIMAL(10,0) {0}", this.IsNull);
                    default:
                        return "";
                }
            }
        }
    }
}
