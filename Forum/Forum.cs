using System;
using System.Collections.Generic;

namespace Forum
{
    public class Forum
    {
        //Forum model
        public int ForumId { get; set; }
        public string ForumName { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }

        private ForumRepository ForumRepository { get; set; }

        public Forum()
        {

        }

        public Forum(string dbPath)
        {
            ForumRepository = new ForumRepository(dbPath);
        }

        public List<Forum> GetForums()
        {
            return ForumRepository.GetAllForums();
        }

        public bool CreateForum(string forumName, User user)
        {
            return ForumRepository.CreateForum(forumName, user);
        }

        public bool DeleteForum(int forumIdToDelete)        
        {
            return ForumRepository.DeleteForum(forumIdToDelete);
        }
    }
}
