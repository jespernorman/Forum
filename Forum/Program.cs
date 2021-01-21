using System;
using System.Linq;

namespace Forum
{
    class Program
    {

        private const string dbPath = "../../../Database/Forum.db";
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hej! Välkommen till detta forum.");
            var forum = new Forum(dbPath);
            var post = new Post(dbPath);    
            var user = new User(dbPath);
            //user.LoadAllUsers();

            string userName;
            string passWord;

            var exitCommand = false;
            
            while (exitCommand != true)
            {
                Console.WriteLine("Tryck 1 för att logga in");
                Console.WriteLine("Tryck 2 för att skapa ett konto");
                Console.WriteLine("Tryck x för att avsluta");
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

                        HandleForum(user,forum, post);

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
                else if (inmatning == "x")
                {
                    exitCommand = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Det du matade in var inte giltigt, pröva igen!");
                }
            }
        }

        public static void HandleForum(User user, Forum forum, Post post)
        {
            string forumIdInmatning = "";
            int choosenForumId = 0;

            while (forumIdInmatning != "x")
            {
                var forums = forum.GetForums();

                Console.WriteLine("Listar alla forum:");

                foreach (var _forum in forums)
                {
                    Console.WriteLine("Forum Id: " + _forum.ForumId.ToString() + " forum namn: " + _forum.ForumName + " forumet är skapat: " + _forum.CreateDate);
                }

                Console.WriteLine("Välj 1 om du vill skriva i befintlig tråd, välj 2 om du vill skapa en ny, 3 om du vill radera en tråd.");

                var val = Console.ReadLine();

                if (int.Parse(val) == 1)
                {
                    Console.WriteLine("Välj vilken tråd du vill öppna med att mata in forum Id:t");
                    forumIdInmatning = Console.ReadLine();
                    choosenForumId = int.Parse(forumIdInmatning);

                    if (forums.Any(x => x.ForumId == choosenForumId))
                    {
                        while (forumIdInmatning != "b")
                        {
                            var choosenForum = forums.FirstOrDefault(x => x.ForumId == choosenForumId);

                            var listOfPosts = post.GetPostsByForumId(choosenForumId);

                            foreach (var _post in listOfPosts)
                            {
                                Console.WriteLine("Forum Id: " + _post.ForumId.ToString() + " Post Id: " + _post.PostId.ToString() + " post text: " + _post.PostText + " posten är skapat: " + _post.CreateDate + " av userid:" + _post.UserId.ToString());
                            }

                            Console.WriteLine("Tryck 1 om du vill lägga till en post i tråden, 2 för att redigera en post, 3 för att ta bort en post, b för att backa till föregående menu eller x för att avsluta");

                            var addPostCommand = Console.ReadLine();

                            if (addPostCommand == "x")
                            {
                                Environment.Exit(0);
                            }

                            if (addPostCommand == "b")
                            {
                                forumIdInmatning = "b";
                                continue;
                            }

                            if (addPostCommand == "1")
                            {
                                Console.WriteLine("Mata in det du vill skriva i tråden");
                                string PostText = Console.ReadLine();
                                post.CreatePost(choosenForum.ForumId, user.UserId, PostText);
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Din post är nu postad!");
                                continue;
                            }

                            if( addPostCommand == "2")          //Nytt
                            {
                                Console.WriteLine("Mata in det post id du vill redigera");
                                string textchosenPostId = Console.ReadLine();
                                int chosenPostId = int.Parse(textchosenPostId);

                                Console.WriteLine("Skriv in det du vill skriva istället.");
                                string newPostText = Console.ReadLine();
                                post.UpdatePost(chosenPostId, newPostText);

                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Din post är nu updaterad!");
                                continue;
                            }

                            if( addPostCommand == "3")              //nytt
                            {
                                Console.WriteLine("Mata in post id:t på den post du vill radera");
                                string textchosenPostId = Console.ReadLine();
                                int chosenPostId = int.Parse(textchosenPostId);
                                post.DeletePost(chosenPostId);

                                Console.WriteLine("Posten är nu raderad");
                                continue;
                            }

                            else
                            {
                                Console.WriteLine("Det du matade in var inte giltigt");

                            }
                        }
                    }
                }

                else if (int.Parse(val) == 2)
                {
                    Console.WriteLine("Vad ska tråden heta?");
                    string forumName = Console.ReadLine();

                    forum.CreateForum(forumName, user);
                    Console.WriteLine("Din tråd är nu skapad! :)");

                    continue;
                }

                else if (int.Parse(val) == 3)                   //nytt
                {
                    Console.WriteLine("Skriv in det forum id du vill radera.");
                    string textChosenForumId = Console.ReadLine();
                    int forumIdToDelete = int.Parse(textChosenForumId);
                    forum.DeleteForum(forumIdToDelete);

                    Console.WriteLine("Tråden är nu raderad");

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