using System;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Forum
{
    class Program
    {
        static void Main(string[] args)
        {
            var databasemanager = new DatabaseManager();
            var forum = new Forum();
            var post = new Post();
            var user = new User();

            databasemanager.GetData();

            post.GetPost();

            user.GetUser();

        }
    }
}
