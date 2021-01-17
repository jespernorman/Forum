using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Forum
{
    public class Forum
    {
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }

        //public List<Forum> listOfForums = new List<Forum>();
        private string DBPath { get; set; }
        private ForumRepository ForumRepository { get; set; }

        public Forum()
        {

        }

        public Forum(string dbPath)
        {
            DBPath = dbPath;
            ForumRepository = new ForumRepository(dbPath);
        }

        public List<Forum> GetForums()
        {
            return ForumRepository.GetAll();
        }

        public bool CreateForum(string forumName, User user)
        {
            return ForumRepository.AddForum(forumName, user);
        }

        public bool DeleteForum(int forumIdToDelete)        //ute o cyklar??
        {
            return ForumRepository.DeleteForum(forumIdToDelete);
        }
    }
}
