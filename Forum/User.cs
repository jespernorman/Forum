using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class User
    {
        public int User_Id { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<User> UserList = new List<User>();

        private const string _connectionString = "Data Source=.//Forum.db";

        public User()
        {
          
        }

        public User GetAllUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            var query = "SELECT * FROM User WHERE User";

            return connection.QuerySingle<User>(query);
        }

        public void CreateUser()
        {

            Console.WriteLine("skriv in User id");
            User_Id = int.Parse(Console.ReadLine());

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
