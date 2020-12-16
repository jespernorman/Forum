using System;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class Post
    {
        public int Post_Id { get; set; }
        public int Forum_Id { get; set; }
        public string Post_text { get; set; }
        public DateTime Create_Date { get; set; }

        private const string _connectionString = "Data Source=.//Forum.db";

        public Post()
        {

        }

        public Post GetPost()
        {

            using var connection = new SqliteConnection(_connectionString);
            var query = "SELECT * FROM Post WHERE Post";

            return connection.QuerySingle<Post>(query);
        }
    }
}
