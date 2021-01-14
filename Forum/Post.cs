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
        public int PostCount { get; set; }

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

            //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            //{
            //    connection.Open();

            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //          SELECT *
            //          FROM  Post
            //          WHERE Forum_Id = $id
            //    ";
            //    command.Parameters.AddWithValue("$id", chosenForumId);

            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var post = new Post();

            //            var postId = reader.GetString(0);
            //            var forumId = reader.GetString(1);
            //            var postText = reader.GetString(2);
            //            var createDate = reader.GetString(3);
            //            var userId = reader.GetString(4);

            //            post.PostId = int.Parse(postId);
            //            post.ForumId = int.Parse(forumId);
            //            post.PostText = postText;
            //            post.CreateDate = DateTime.Parse(createDate);
            //            post.UserId = int.Parse(userId);

            //            listOfPosts.Add(post);
            //        }
            //    }
            //}

            return listOfPosts;
        }

        public List<Post> GetPostsByForumAndUser(int choosenForumId,int choosenUserId)
        {
            var listOfPosts = new List<Post>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            //{
            //    connection.Open();

            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //          SELECT P.Post_Id,P.Forum_Id,P.Post_Text,P.Create_Date,P.User_Id
            //          FROM  Post as P
            //          INNER JOIN Forum as F on P.Forum_Id = F.Forum_Id
            //          INNER JOIN User as U on U.User_Id = P.User_Id
            //          WHERE F.Forum_Id = $id and U.User_Id = $userId
            //    ";
            //    command.Parameters.AddWithValue("$id", choosenForumId);
            //    command.Parameters.AddWithValue("$userId", choosenUserId);

            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            var post = new Post();

            //            var postId = reader.GetString(0);
            //            var forumId = reader.GetString(1);
            //            var postText = reader.GetString(2);
            //            var createDate = reader.GetString(3);
            //            var userId = reader.GetString(4);

            //            post.PostId = int.Parse(postId);
            //            post.ForumId = int.Parse(forumId);
            //            post.PostText = postText;
            //            post.CreateDate = DateTime.Parse(createDate);
            //            post.UserId = int.Parse(userId);

            //            listOfPosts.Add(post);
            //        }
            //    }
            //}

            return listOfPosts;
        }


        public void CreatePost(Forum forum, User user, string postText)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            //{
            //    connection.Open();

            //    //Insert data:
            //    using (var transaction = connection.BeginTransaction())
            //    {
            //        var insertCmd = connection.CreateCommand();

            //        insertCmd.CommandText = "INSERT INTO Post(Forum_Id, Post_Text, Create_Date, User_Id) values('" + forum.ForumId + "', '" + postText + "', '" + DateTime.Now + "', '" + user.UserId + "'); ";
            //        insertCmd.ExecuteNonQuery();
            //        transaction.Commit();
            //    }
            //}

        }
        public void UpdatePost(int chosenPostId, User user, string newPostText)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            //{
            //    connection.Open();

            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //          UPDATE Post SET Post_Text = $newPostText
            //          WHERE Post_Id = $id
            //    ";
            //    command.Parameters.AddWithValue("$id", chosenPostId);
            //    command.Parameters.AddWithValue("$newPostText", newPostText);
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //}
        }

        public void DeletePost(int chosenPostId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            //{
            //    connection.Open();

            //    var command = connection.CreateCommand();
            //    command.CommandText =
            //    @"
            //          DELETE FROM Post
            //          WHERE Post_Id = $id
            //    ";
            //    command.Parameters.AddWithValue("$id", chosenPostId);
            //    command.ExecuteNonQuery();
            //    connection.Close();
            //}
        }
    }
}
