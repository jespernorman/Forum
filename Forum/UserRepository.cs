﻿using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class UserRepository
    {
        private string DBPath { get; set; }

        public UserRepository()
        {

        }

        public UserRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<User> GetAllUsers()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var UserList = new List<User>();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                UserList = connection.Query<User>("SELECT * FROM User").AsList();
            }

            return UserList;
        }

        public User GetUserById(int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var user = new User();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                user = connection.QueryFirst<User>("SELECT * FROM User WHERE id=@id", new { id = userId });
            }

            return user;
        }

        public User GetUserByName(int userName)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var user = new User();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                user = connection.QueryFirst<User>("SELECT * FROM User WHERE id=@id", new { id = userName });
            }

            return user;
        }

        public bool CreateUser(string userName, string passWord)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO User(UserName, PassWord, Create_Date) VALUES(@UserName, @PassWord, @Create_Date)";

                var dp = new DynamicParameters();
                dp.Add("@UserName", userName, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@PassWord", passWord);
                dp.Add("@Create_Date", DateTime.Now);

                insertedRow = connection.Execute(query, dp);

            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;

        }

        public bool DeleteUser(int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM User WHERE User_Id = @Id", new { Id = userId });
            }

            if (delRows > 0)
                return true;

            return false;
        }

        public bool UpdateUser(int userId, string userName, string passWord)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE User SET UserName = @userName,passWord = @passWord WHERE User_Id = @userId", new { userName, passWord, userId });
            }

            if (updatedRows > 0)
                return true;

            return false;
        }
    }
}
