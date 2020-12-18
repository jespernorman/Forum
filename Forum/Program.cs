using System;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Forum
{
    class Program
    {

        private const string dbPath = "../../../Database/Forum.db";
        //private const string dbPath = "/Users/tomnor/projects/Forum/Forum/Database/Forum.db";

        static void Main(string[] args)
        {
            var forum = new Forum(dbPath);

            //Testar att lista ut alla forum från databasen
            var forums = forum.GetForums();

            Console.WriteLine("Listar alla forum:");

            foreach (var _forum in forums)
            {
                Console.WriteLine("Forum Id: " + _forum.ForumId.ToString() + " forum namn: " + _forum.ForumName + " forumet är skapat: " + _forum.CreateDate);
            }

            //Testar att lista ut alla poster från databasen
            var post = new Post(dbPath);

            var posts = post.GetPosts();

            Console.WriteLine("Listar alla poster:");

            foreach (var _post in posts)
            {
                Console.WriteLine("Post Id:" + _post.PostId  + "Forum Id: " + _post.ForumId + "user Id: " + _post.UserId +  " inlägg: " + _post.PostText + " post är skapat: " + _post.CreateDate);
            }

            //Lägg till post
            var newPost = new Post(dbPath);
            newPost.UserId = 1;
            newPost.ForumId = 1;
            newPost.CreateDate = DateTime.Now;
            newPost.PostText = "Hej Jeppe läget???";
            newPost.CreatePost();

            //Hämta användare baserat på användar namn och lösen ord
            var user = new User(dbPath);


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