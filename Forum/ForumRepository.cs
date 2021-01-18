using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class ForumRepository
    {
        public List<Forum> ListOfForums = new List<Forum>();

        private string DBPath { get; set; }

        public ForumRepository()
        {

        }

        public ForumRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<Forum> GetAll()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var ForumList = new List<Forum>(); //?? inte listofforum?

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                ListOfForums = connection.Query<Forum>("SELECT * FROM Forum").AsList();
            }

            return ListOfForums;
        }

        public Forum GetById(int forumId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var forum = new Forum();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                forum = connection.QueryFirst<Forum>("SELECT * FROM Forum WHERE id=@id", new { id = forumId });
            }

            return forum;
        }

        public bool AddForum(string forumName, User user)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Forum(Forum_Name, User_Id, Create_Date) VALUES(@Forum_Name, @User_Id, @Create_Date)";

                var dp = new DynamicParameters();
                dp.Add("@Forum_Name", forumName, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@User_Id", user.UserId);
                dp.Add("@Create_Date", DateTime.Now);

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
                delRows = connection.Execute(@"DELETE FROM Forum WHERE Id = @Id", new { Id = forumId });
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
                updatedRows = connection.Execute("UPDATE Forum SET forum_Name = @forumName,User_Id = @userId WHERE Forum_Id = @forumId", new { forumName, userId, forumId });
            }

            if (updatedRows > 0)
                return true;

            return false;
        }
    }
}
