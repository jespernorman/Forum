using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class UserRepository
    {
        public List<User> UserList = new List<User>();
        public UserRepository()
        {

        }

       

        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        SqliteConnectionStringBuilder.DataSource = DBPath;

        public void GetAllUsers()
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

                        //UserList.Add(user);??
                    }
                }
            }
        }
        public void AddUserToRepository()
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
        }
    }

}
