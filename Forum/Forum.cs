using System;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Forum
{
    public class Forum
    {
        public int Forum_Id { get; set; }
        public int Forum_Name { get; set; }
        public DateTime Create_Date { get; set; }

        private const string _connectionString = "Data Source=.//Forum.db";

        public Forum()
        {
        }

        public Forum GetThreds()
        {
            using var connection = new SqliteConnection(_connectionString);
            var query = "SELECT * FROM Forum WHERE Forum";

            return connection.QuerySingle<Forum>(query);
        }
    }
}
