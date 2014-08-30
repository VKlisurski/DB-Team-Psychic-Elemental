namespace MySqlDbModule
{
    using MySql.Data.MySqlClient;
    using System;

    public class MySqlDbCreator
    {
        internal const string UserName = "root";
        internal const string Password = "12345";
        private const string DbName = "sales_reports_database";

        public static void CreateBatabase()
        {
            DropDatabaseIfExists(DbName);
            CreateDatabase(DbName);

            TableBuilder tb = new TableBuilder();
            tb.BuildAllTables();
        }

        private static void CreateDatabase(string databaseName)
        {
            Console.WriteLine("Creating database {0}", databaseName);

            MySqlConnection connection = new MySqlConnection(string.Format("Data Source=localhost;UserId={0};PWD={1};", UserName, Password));

            string commandText = string.Format("CREATE SCHEMA IF NOT EXISTS `{0}` DEFAULT CHARACTER SET latin1; USE `{0}`; ", databaseName);

            MySqlCommand command = new MySqlCommand(commandText, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static void DropDatabaseIfExists(string databaseName)
        {
            Console.WriteLine("Removing previous instances of {0}", databaseName);

            MySqlConnection connection = new MySqlConnection(string.Format("Data Source=localhost;UserId={0};PWD={1};", UserName, Password));

            string commandText = string.Format("DROP DATABASE IF EXISTS `{0}`; ", databaseName);

            MySqlCommand command = new MySqlCommand(commandText, connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
