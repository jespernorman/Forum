using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
namespace Forum
{
    public class Forum
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public DateTime CreateDate { get; set; }

        private const string _connectionString = "/Users/tomnor/projects/Forum/Forum/Database/Forum.db";

        public Forum()
        {

        }

        public List<Forum> GetForums()
        {

            var listOfForums = new List<Forum>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "/Users/tomnor/projects/Forum/Forum/Database/Forum.db";

            var forumId = 1;
            
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM Forum
                ";

                //command.CommandText =
                //@"
                //    SELECT *
                //    FROM Forum
                //    WHERE Forum_Id = $id
                //";

                //command.Parameters.AddWithValue("$id", forumId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var forum = new Forum();

                        var id = reader.GetString(0);
                        var name = reader.GetString(1);
                        var createDate = reader.GetString(2);

                        forum.ForumId = int.Parse(id);
                        forum.ForumName = name;
                        forum.CreateDate = DateTime.Parse(createDate);

                        listOfForums.Add(forum);
                    }
                }
            }

            return listOfForums;

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
