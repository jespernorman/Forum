using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;
namespace Forum
{
    public class Forum
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public DateTime CreateDate { get; set; }

        public List<Forum> ListAllForum = new List<Forum>();

        private const string _connectionString =
            "Data Source=/Users/jespernorman/Projects/workspace/oop_csharp/Forum/Database/Forum.db";

        public Forum()
        {

        }

        public Forum GetThreds()
        {
            //using var connection = new SqliteConnection(_connectionString);
            //var query = "SELECT * FROM Forum";

            //var dataFromForum = connection.QuerySingle<Forum>(query);

            //return dataFromForum;

            //string connStr = "server=localhost;user=root;database=world;port=3306;password=******";

            var forumId = 1;
            var forum = new Forum();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM Forum
                    WHERE Forum_Id = $id
                ";

                command.Parameters.AddWithValue("$id", forumId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetString(0);
                        var name = reader.GetString(1);
                        var createDate = reader.GetString(2);

                        forum.ForumId = int.Parse(id);
                        forum.ForumName = name;
                        forum.CreateDate = DateTime.Parse(createDate);
                        ListAllForum.Add(forum);
                    }
                }
            }

            return forum;

        }

        public void CreateThread()
        {
            ForumId = 1;
            Console.WriteLine("Vad ska din tråd heta?");
            ForumName = Console.ReadLine();
            CreateDate = DateTime.Now;
        }
    }
}
