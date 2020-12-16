using System;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class User
    {
        public int User_ID { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }

        private const string _connectionString = "Data Source=.//Forum.db";

        public User()
        {

        }

        public User GetUser()
        {
            using var connection = new SqliteConnection(_connectionString);
            var query = "SELECT * FROM User WHERE User";

            return connection.QuerySingle<User>(query);
        }
    }
}
