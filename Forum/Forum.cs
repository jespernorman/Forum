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
        public int UserId { get; set; }

        public List <Forum> listOfForums = new List<Forum>();
        private string DBPath { get; set; }

        public Forum(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<Forum> GetForums()
        {

            listOfForums = new List<Forum>();

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

                        listOfForums.Add(forum);
                    }
                }
            }

            return listOfForums;

        }

        public void CreateForum(string forumName, User user)
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

                    insertCmd.CommandText = "INSERT INTO Forum(Forum_Name, User_Id, Create_Date) values('" + forumName + "', '" + user.UserId + "', '" + DateTime.Now + "'); ";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }
        public void DeleteForum(int forumIdToDelete)        //ute o cyklar??
        {
            listOfForums = new List<Forum>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

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
