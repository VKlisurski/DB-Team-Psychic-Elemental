namespace MySqlDbModule
{
    using System.Data;

    using MySql.Data.MySqlClient;

    public static class MySqlDb
    {
        private static readonly string connectionStr = string.Format("Server=localhost;Port=3306;Database=sales_reports_database;Uid={0};Pwd={1};", MySqlDbCreator.UserName, MySqlDbCreator.Password);
        private static MySqlConnection connection;
        private static MySqlCommand command;

        public static MySqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(connectionStr);

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                }

                return connection;
            }
        }

        public static MySqlCommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new MySqlCommand();
                    command.Connection = Connection;
                }

                return command;
            }
        }

        public static MySqlDataReader GetData(string myQuery)
        {
            var myCommand = MySqlDb.Command;
            myCommand.CommandText = myQuery;

            return myCommand.ExecuteReader();
        }

        public static int Exec(string myQuery)
        {
            var myCommand = MySqlDb.Command;
            myCommand.CommandText = myQuery;

            return myCommand.ExecuteNonQuery();
        }
    }
}
