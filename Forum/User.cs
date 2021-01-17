using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime CreateDate { get; set; }

        public List<User> UserList = new List<User>();

        private string DBPath { get; set; }
        private UserRepository UserRepository { get; set; }


        public User()
        {

        }

        public User(string dbPath)
        {
            DBPath = dbPath;
            UserRepository = new UserRepository(dbPath);
            LoadAllUsers();
        }

        public void LoadAllUsers()
        {
            var userRepository = new UserRepository();
            UserList = userRepository.GetAll();
        }

        public bool CreateUser(string userName, string passWord)
        {
            if (UserList.Any(x => x.UserName == UserName))
            {
                return false;
            }
            else
            {
                UserRepository.AddUser(userName, passWord);
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
                UserId = loggedInUser.UserId;
                CreateDate = loggedInUser.CreateDate;
                UserName = loggedInUser.UserName;
                return true;                
            }
            else
            {
                return false;
            }
        }
    }
}
