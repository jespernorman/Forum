using System;
using Microsoft.Data.Sqlite;
namespace Forum
{
    public class DatabaseManager
    {
        private const string _connectionString = "Data Source=.//Forum.db";

        public DatabaseManager()
        {

        }

        public void GetData()
        {
            using var connection = new SqliteConnection(_connectionString);
            System.Console.WriteLine(connection.ServerVersion);
        }
    }

}
