using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class ForumRepository
    {
        public List<Forum> ListOfForums = new List<Forum>();

        public ForumRepository()
        {

        }

        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        DbConnectionStringBuilder.DataSource = DBPath;

        public void LoadAllForumFromRepository()
        {
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
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var forum = new Forum(DBPath);
                        var id = reader.GetString(0);
                        var name = reader.GetString(1);
                        var userid = reader.GetString(2);
                        var createDate = reader.GetString(3);

                        forum.ForumId = int.Parse(id);
                        forum.ForumName = name;
                        forum.UserId = int.Parse(userid);
                        forum.CreateDate = DateTime.Parse(createDate);

                        ListOfForums.Add(forum);
                    }
                }
            }
        }
        public void CreateForumToRepository()
        {
            var forum = new Forum();
            var user = new User();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Insert data:
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertCmd.CommandText = "INSERT INTO Forum(Forum_Name, User_Id, Create_Date) values('" + forum.ForumName + "', '" + user.UserId + "', '" + DateTime.Now + "'); ";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }

        public void DeleteForumFromRepository(int forumIdToDelete)
        {
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM Forum
                    WHERE FORUM_ID = $id
                ";
                command.Parameters.AddWithValue("$id", forumIdToDelete);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
