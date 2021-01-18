using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class PostRepository
    {
        public List<Post> ListOfPosts = new List<Post>();

        private string DBPath { get; set; }

        public PostRepository(string dbPath)
        {
            DBPath = dbPath;
        }


        public List<Post> LoadAllPosts(int chosenForumId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var postsLists = new List<Post>();
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                ListOfPosts = connection.Query<Post>("SELECT * FROM Post").AsList();
            }
            return ListOfPosts;
        }

            //using ( var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))

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

            //            ListOfPosts.Add(post);
            //        }
            //    }
            
    
        //public List<Post> LoadPostsByForumAndUserToRepository(int choosenForumId, int choosenUserId)
        //{
        //    using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
        //    {
        //        connection.Open();

        //        var command = connection.CreateCommand();
        //        command.CommandText =
        //        @"
        //              SELECT P.Post_Id,P.Forum_Id,P.Post_Text,P.Create_Date,P.User_Id
        //              FROM  Post as P
        //              INNER JOIN Forum as F on P.Forum_Id = F.Forum_Id
        //              INNER JOIN User as U on U.User_Id = P.User_Id
        //              WHERE F.Forum_Id = $id and U.User_Id = $userId
        //        ";
        //        command.Parameters.AddWithValue("$id", choosenForumId);
        //        command.Parameters.AddWithValue("$userId", choosenUserId);

        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var post = new Post();

        //                var postId = reader.GetString(0);
        //                var forumId = reader.GetString(1);
        //                var postText = reader.GetString(2);
        //                var createDate = reader.GetString(3);
        //                var userId = reader.GetString(4);

        //                post.PostId = int.Parse(postId);
        //                post.ForumId = int.Parse(forumId);
        //                post.PostText = postText;
        //                post.CreateDate = DateTime.Parse(createDate);
        //                post.UserId = int.Parse(userId);

        //                ListOfPosts.Add(post);
        //            }
        //        }

        //    }
        //}
        public bool AddPost(int forumId, int userId, string postText) //USER user?
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Post(Forum_Id, Post_Text, Create_Date, User_Id) VALUES(@Forum_Id, @Post_Text, @Create_Date";

                var dp = new DynamicParameters();
                dp.Add("@Forum_Id", forumId);
                dp.Add("@Post_Text", postText, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@Create_Date", DateTime.Now);
                dp.Add("@User_Id", userId);

                insertedRow = connection.Execute(query, dp);
            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;
        }
    //var forum = new Forum();
    //var post = new Post();
    //var user = new User();

    //using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
    //{
    //    connection.Open();

    //    //Insert data:
    //    using (var transaction = connection.BeginTransaction())
    //    {
    //        var insertCmd = connection.CreateCommand();

    //        insertCmd.CommandText = "INSERT INTO Post(Forum_Id, Post_Text, Create_Date, User_Id) values('" + forum.ForumId + "', '" + post.PostText + "', '" + DateTime.Now + "', '" + user.UserId + "'); ";
    //        insertCmd.ExecuteNonQuery();
    //        transaction.Commit();
    //    }
    //}

        public bool EditPost(int chosenPostId, string newPostText)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
              connection.Open();
              updatedRows = connection.Execute("UPDATE Post SET Post_Text = @Post_Text WHERE Post_Id = @postId", new { newPostText, chosenPostId });
            }

            if (updatedRows > 0)
            {
               return true;
            }

            return false;
        }
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
    

        public bool DeleteChosenPost(int chosenPostId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {            connection.Open();
                 delRows = connection.Execute(@"DELETE FROM Post WHERE Id = @Id", new { Id = chosenPostId });
            }

            if (delRows > 0)
            {//?
               return true;
            }
            
        return false;
        }

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

        
    }
}
