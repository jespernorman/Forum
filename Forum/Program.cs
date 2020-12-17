using System;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Forum
{
    class Program
    {
        static void Main(string[] args)
        {
            var forum = new Forum();

            //Testar att lista ut alla forum från databasen
            var forums = forum.GetForums();

            Console.WriteLine("Listar alla forum:");

            foreach (var _forum in forums)
            {
                Console.WriteLine("Forum Id: " + _forum.ForumId.ToString() + " forum namn: " + _forum.ForumName + " forumet är skapat: " + _forum.CreateDate);
            }
            
            var post = new Post();
            var user = new User();

            //post.GetPost();
            //user.GetUser();

            Console.WriteLine("Hej! välkommen till detta forum.");

            while (false)
            {
                Console.WriteLine("Tryck 1 för att logga in");
                Console.WriteLine("Tryck 2 för att skapa ett konto");
                string inmatning = (Console.ReadLine());

                if (inmatning == "1")
                {
                    user.Login();
                    continue;
                }
                if (inmatning == "2")
                {
                    user.CreateUser();
                    continue;
                }
                else
                {
                    Console.WriteLine("Det du matade in var inte giltigt.");
                }
            }
        }
    }
}