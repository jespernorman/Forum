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

        public void CreatePost()
        {
            if(Forum_Id != null)
            {
                Console.WriteLine("Skriv in vad du vill posta");
                Post_text = Console.ReadLine();
               // Post_Id;?
               // Create_Date;?
            }
            else
            {
                Console.WriteLine("Tråden existerar inte");
            }

        }
    }
}
