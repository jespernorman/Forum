using System;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Linq;

namespace Forum
{
    class Program
    {

        private const string dbPath = "../../../Database/Forum.db";
        

        static void Main(string[] args)
        {

            ////Testar att lista ut alla poster från databasen
            //var post = new Post(dbPath);
            //var posts = post.GetPosts(1);

            ////Console.WriteLine("Listar alla poster:");

 

            ////Lägg till post
            //var newPost = new Post(dbPath);
            //newPost.UserId = 1;
            //newPost.ForumId = 1;
            //newPost.CreateDate = DateTime.Now;
            //newPost.PostText = "Hej Jeppe läget???";
            //newPost.CreatePost();

            ////Hämta användare baserat på användar namn och lösen ord
            ////var user = new User(dbPath);


            Console.WriteLine("Hej! Välkommen till detta forum.");
            var forum = new Forum(dbPath);
            var post = new Post();
            
            var user = new User(dbPath);
            user.LoadAllUsers();

            string userName;
            string passWord;
            
            while (!user.Login(userName, passWord || user.CreateUser(userName, passWord))
            {
                Console.WriteLine("Tryck 1 för att logga in");
                Console.WriteLine("Tryck 2 för att skapa ett konto");
                string inmatning = Console.ReadLine();

                if (inmatning == "1")
                {
                    Console.WriteLine(" Skriv in ditt användarnamn");
                    userName = Console.ReadLine();
                    Console.WriteLine("Skriv in ditt lösenord");
                    passWord = Console.ReadLine();

                    if(!user.Login(userName, passWord))
                    {
                        Console.WriteLine("Användarnamnet och lösenordet stämmer inte överens.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Du är nu inloggad.");
                    }

                    continue;
                }

                if (inmatning == "2")
                {
                    
                    Console.WriteLine("skriv in användarnamn");
                    userName = Console.ReadLine();

                    Console.WriteLine("skriv in ett lösenord");
                    passWord = Console.ReadLine();

                    user.CreateUser(userName, passWord);
                    Console.WriteLine("Du har nu lagt till en användare");

                    continue;
                }

                else
                {
                    Console.WriteLine("Det du matade in var inte giltigt.");
                }
            }

            Console.WriteLine("Du är nu inloggad, Tryck x för att avsluta och logga ut.");

            string forumIdInmatning = "";
            int chosenForumId = 0;

            while (forumIdInmatning != "x")
            {
                var forums = forum.GetForums();

                Console.WriteLine("Listar alla forum:");

                foreach (var _forum in forums)
                {
                    Console.WriteLine("Forum Id: " + _forum.ForumId.ToString() + " forum namn: " + _forum.ForumName + " forumet är skapat: " + _forum.CreateDate);
                }
           
                Console.WriteLine("Välj vilken tråd du vill öppna med att mata in forum Id:t");
                Console.WriteLine("Om du vill skapa en ny tråd tryck 2");

                forumIdInmatning = Console.ReadLine();
                chosenForumId = int.Parse(forumIdInmatning);

                if (forum.listOfForums.Any(x => x.ForumId == chosenForumId))                   
                {
                    while(forumIdInmatning != "x")
                    {
                        forum.listOfForums.FirstOrDefault(x => x.ForumId == chosenForumId);

                        post.GetPosts(chosenForumId);

                        Console.WriteLine("Tryck 1 om du vill lägga till en post i tråden, b för att backa.");
                        string addPostCommand = Console.ReadLine();

                        if (addPostCommand == "x")
                        {
                            Environment.Exit(0);
                        }
                        if (addPostCommand == "b")
                        {
                            continue;
                        }
                        if (addPostCommand == "1")
                        {
                            Console.WriteLine("Mata in det du vill skriva i tråden");
                            string PostText = Console.ReadLine();
                            post.CreatePost(PostText);
                            Console.WriteLine("Din post är nu postad!");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Det du matade in var inte giltigt");
                           
                        }
                    }
                }

                else if(chosenForumId == 2)
                {
                    Console.WriteLine("Vad ska tråden heta?");
                    string forumName = Console.ReadLine();

                    forum.CreateForum(forumName);
                    Console.WriteLine("Din tråd är nu skapad! :)");

                    continue;
                }
                else
                {
                    Console.WriteLine("Det du matade in var inte giltigt. :)");

                }

            }

            //Console.WriteLine("Tryck 1 om du vill lägga till en post i tråden");
            //string addPostCommand = Console.ReadLine();
            
            //if (addPostCommand == "x")
            //{
            //    Environment.Exit(0);
            //}
            //if (addPostCommand == "1")
            //{
            //    Console.WriteLine("Mata in det du vill skriva");
            //    string PostText = Console.ReadLine(); 
            //    post.CreatePost(PostText);
            //}
            //else
            //{
            //    Console.WriteLine("Det du matade in var inte giltigt");
            //}

            //var posts = post.GetPosts(chosenForumId);
            //foreach (var _post in posts)
            //{
            //    Console.WriteLine("Post Id:" + _post.PostId + "Forum Id: " + _post.ForumId + "user Id: " + _post.UserId + " inlägg: " + _post.PostText + " post är skapat: " + _post.CreateDate);
            //}
        }
    }
}