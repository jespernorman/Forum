using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        private string PassWord { get; set; }
        public DateTime CreateDate { get; set; }

        public List<User> UserList = new List<User>();

        private string DBPath { get; set; }

        public User()
        {

        }

        public User(string dbPath)
        {
            DBPath = dbPath;
            LoadAllUsers();
        }

        public void LoadAllUsers()
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
                    FROM User
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();

                        var userId = reader.GetString(0);
                        var userName = reader.GetString(1);
                        var password = reader.GetString(2);
                        var createDate = reader.GetString(3);
                        

                        user.UserId = int.Parse(userId);
                        user.UserName = userName;
                        user.PassWord = password;
                        user.CreateDate = DateTime.Parse(createDate);

                        UserList.Add(user);
                    }
                }
            }
        }

        public bool CreateUser(string userName, string passWord)
        {
            var user = new User();
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            if (UserList.Any(x => x.UserName == UserName))
            {
                return false;
            }
            else
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();

                        insertCmd.CommandText = "INSERT INTO User(userName, passWord, Create_Date) values('" + userName + "', '" + passWord + "', '" + DateTime.Now + "'); ";
                        insertCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
                LoadAllUsers();

              return true;
            }
        }

        public bool Login(string userName, string passWord)
        {

            if (UserList.Any(x => x.UserName == userName && x.PassWord == passWord))
            {
                var loggedInUser = UserList.FirstOrDefault(x => x.UserName == userName && x.PassWord == passWord);
                // Set the properties with the logged in user
                this.UserId = loggedInUser.UserId;
                this.CreateDate = loggedInUser.CreateDate;
                this.UserName = loggedInUser.UserName;
                return true;                
            }
            else
            {
                return false;
            }
        }
    }
}
