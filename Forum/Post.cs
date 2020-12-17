using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class Post
    {
        public int PostId { get; set; }
        public int ForumId { get; set; }
        public string PostText { get; set; }
        public DateTime CreateDate { get; set; }

        public List<Post> ListAllPost = new List<Post>();
        public List<User> UserList = new List<User>();

        private const string _connectionString = "Data Source=.//Forum.db";

        public Post()
        {

        }

        public Post GetPost()
        {
            //using var connection = new SqliteConnection(_connectionString);
            //var query = "SELECT * FROM Post WHERE Post";

            //return connection.QuerySingle<Post>(query);
            var post = new Post();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT *
                    FROM Post
                    WHERE Forum_Id = $id
                ";

                command.Parameters.AddWithValue("$id", PostId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetString(0);
                        var forumid = reader.GetString(1);
                        var posttext = reader.GetString(2);
                        var createDate = reader.GetString(3);

                        post.PostId = int.Parse(id);
                        post.ForumId = int.Parse(forumid);
                        post.PostText = posttext;
                        post.CreateDate = DateTime.Parse(createDate);
                        ListAllPost.Add(post);
                    }
                }

                return post;
            }
        }
           

        public void CreatePost()
        {
            //var post = new Post();
            //if (ForumId != null)
            //{
            //    Console.WriteLine("Skriv in vad du vill posta");
            //    Posttext = Console.ReadLine();
            //    // Post_Id;?
            //    post.CreateDate = DateTime.Parse(CreateDate);
            //}
            //else
            //{
            //    Console.WriteLine("Tråden existerar inte");
            //}

        }
    }
}
