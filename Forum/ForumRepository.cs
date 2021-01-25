using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class ForumRepository
    {

        private string DBPath { get; set; }

        public ForumRepository()
        {

        }

        public ForumRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<Forum> GetAllForums()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            connection.Open();
            var result = connection.Query<Forum>("SELECT * FROM Forum").AsList();
            return result;
        }

        public Forum GetForumById(int forumId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var forum = new Forum();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                forum = connection.QueryFirst<Forum>("SELECT * FROM Forum WHERE ForumId=@ForumId", new { ForumId = forumId });
            }

            return forum;
        }

        public bool CreateForum(string forumName, User user)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Forum(ForumName, UserId, CreateDate) VALUES(@ForumName, @UserId, @CreateDate)";

                var dp = new DynamicParameters();
                dp.Add("@ForumName", forumName, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@UserId", user.UserId);
                dp.Add("@CreateDate", DateTime.Now);

                insertedRow = connection.Execute(query, dp);
            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteForum(int forumId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM Forum WHERE ForumId = @ForumId", new { ForumId = forumId });
            }

            if (delRows > 0)
                return true;

            return false;
        }

        public bool UpdateForum(int forumId, string forumName, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE Forum SET ForumName = @ForumName, UserId = @UserId WHERE ForumId = @ForumId", new { ForumName = forumName, UserId = userId, ForumId = forumId });
            }

            if (updatedRows > 0)
                return true;

            return false;
        }
    }
}
