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
        private string Password { get; set; }
        public bool IsAdmin { get; set; }
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
                        var isAdmin = reader.GetString(3);
                        var createDate = reader.GetString(4);
                        

                        user.UserId = int.Parse(userId);
                        user.UserName = userName;
                        user.Password = password;
                        user.IsAdmin = Convert.ToBoolean(int.Parse(isAdmin));
                        user.CreateDate = DateTime.Parse(createDate);

                        UserList.Add(user);
                    }
                }
            }
        }

        public void CreateUser()
        {

            Console.WriteLine("skriv in User id");
            UserId = int.Parse(Console.ReadLine());

            Console.WriteLine("skriv in användar namn");
            UserName = Console.ReadLine();

            Console.WriteLine("skriv in ett lösenord");
            Password = Console.ReadLine();

            Console.WriteLine("Ska användaren ha admin behörigheter? (Ja/Nej)");
            string isAdmin = Console.ReadLine();
        }

        public bool Login()
        {
            Console.WriteLine("Skriv in användarnamn");
            string username = Console.ReadLine();

            Console.WriteLine("Skriv in lösenord");
            string password = Console.ReadLine();

            if (UserList.Any(x => x.UserName == username))
            {
                UserList.FirstOrDefault((x => x.UserName == username));
                if (Password == password)
                {
                    Console.WriteLine("Du är nu inloggad.");
                    return true;
                }
                else
                {
                    Console.WriteLine("lösenordet stämmer inte, försök igen.");
                }
            }
            else
            {
                Console.WriteLine("Användarnamnet och lösenordet stämmer inte överens");
            }
            return false;
        }
    }
}
