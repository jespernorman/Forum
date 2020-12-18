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
        public int UserId { get; set; }
        public string PostText { get; set; }
        public DateTime CreateDate { get; set; }

        private string DBPath { get; set; }

        public Post()
        {

        }

        public Post(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<Post> GetPosts(int chosenForumId)
        {
            var listOfPosts = new List<Post>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                      SELECT *
                      FROM Forum
                      WHERE Forum_Id = $id
                ";
                command.Parameters.AddWithValue("$id", chosenForumId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var post = new Post();

                        var postId = reader.GetString(0);
                        var forumId = reader.GetString(1);
                        var postText = reader.GetString(2);
                        var createDate = reader.GetString(3);
                        var userId = reader.GetString(4);

                        post.PostId = int.Parse(postId);
                        post.ForumId = int.Parse(forumId);
                        post.PostText = postText;
                        post.CreateDate = DateTime.Parse(createDate);
                        post.UserId = int.Parse(userId);

                        listOfPosts.Add(post);
                    }
                }
            }

            return listOfPosts;
        }


        public void CreatePost(string PostText)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Insert data:
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = "INSERT INTO Post(Post_Id, Forum_Id, Post_Text, Create_Date, User_Id,) values('" + this.PostId+ "', '" + this.ForumId+ "', '" + this.PostText + "', '" + this.CreateDate + "', '" + this.UserId + "'); ";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
            
        }

    }
}
