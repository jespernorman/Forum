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
        private PostRepository PostRepository { get; set; }

        public Post()
        {

        }

        public Post(string dbPath)
        {
            DBPath = dbPath;
            PostRepository = new PostRepository(dbPath);
        }

        public List<Post> GetPosts(int chosenForumId)
        {
            return PostRepository.LoadAllPosts(chosenForumId);
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


        public bool CreatePost(int forumId, User user, string postText)
        {
            return PostRepository.AddPost(forumId, user.UserId, postText); // vilka inparametrar ska jag ha? samma som in i denna metoden eller det jag hittat på i repot??
        }

        public bool UpdatePost(int chosenPostId, string newPostText)
        {
            return PostRepository.EditPost(chosenPostId, newPostText); //vilka e rätt?


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

        public bool DeletePost(int chosenPostId)
        {
        return PostRepository.DeleteChosenPost(chosenPostId);
        }
    }
}
