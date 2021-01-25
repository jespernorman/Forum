using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class PostRepository
    {

        private string DBPath { get; set; }

        public PostRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<Post> GetAllPosts()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var result = connection.Query<Post>("SELECT * FROM Post").AsList();
                return result;
            }
        }

        public List<Post> GetPostsByForumId(int choosenForumId)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var result = connection.Query<Post>("SELECT * FROM Post WHERE ForumId=@ForumId", new { ForumId = choosenForumId }).AsList();
                return result;
            }
        }

        public List<Post> GetPostsByForumAndUser(int choosenForumId, int choosenUserId)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var result = connection.Query<Post>("SELECT * FROM Post WHERE ForumId=@ForumId AND UserId = @UserId", new { ForumId = choosenForumId, UserId = choosenUserId }).AsList();
                return result;
            }
        }

        public List<Post> GetPostsByForumNameAndUserName(string forumName, string userName)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var result = connection.Query<Post>("SELECT P.PostId,P.ForumId,P.PostText,P.CreateDate,P.UserId FROM Post as P " +
                    "INNER JOIN Forum as F on P.ForumId = F.ForumId " +
                    "INNER JOIN User as U on P.UserId = P.UserId" +
                    "WHERE F.ForumName = @ForumName AND U.UserName = @UserName", new { ForumName = forumName, UserName = userName }).AsList();
                return result;
            }
        }

        public Post GetPostById(int postId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var result = connection.QueryFirst<Post>("SELECT * FROM Post WHERE PostId=@Id", new { Id = postId });
                return result;
            }
        }

        public bool CreatePost(int forumId, int userId, string postText) //USER user?
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Post(ForumId, PostText, CreateDate, UserId) VALUES(@ForumId, @PostText, @CreateDate, @UserId)";

                var dp = new DynamicParameters();
                dp.Add("@ForumId", forumId);
                dp.Add("@PostText", postText, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@CreateDate", DateTime.Now);
                dp.Add("@UserId", userId);

                insertedRow = connection.Execute(query, dp);
            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdatePost(int choosenPostId, string newPostText)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
              connection.Open();
              updatedRows = connection.Execute("UPDATE Post SET PostText = @PostText WHERE PostId = @PostId", new { PostText = newPostText, PostId = choosenPostId });
            }

            if (updatedRows > 0)
            {
               return true;
            }

            return false;
        }
    
        public bool DeletePost(int choosenPostId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {            connection.Open();
                 delRows = connection.Execute(@"DELETE FROM Post WHERE PostId = @PostId", new { PostId = choosenPostId });
            }

            if (delRows > 0)
            {
               return true;
            }
            
            return false;
        }
    }
}
