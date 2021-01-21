using System;
using System.Collections.Generic;

namespace Forum
{
    public class Post
    {
        //Post Model
        public int PostId { get; set; }
        public int ForumId { get; set; }
        public int UserId { get; set; }
        public string PostText { get; set; }
        public DateTime CreateDate { get; set; }
        public int PostCount { get; set; }

        //Repository Property
        private PostRepository PostRepository { get; set; }

        public Post()
        {

        }

        public Post(string dbPath)
        {
            PostRepository = new PostRepository(dbPath);
        }

        public List<Post> GetPostsByForumId(int choosenForumId)
        {
            return PostRepository.GetPostsByForumId(choosenForumId);
        }

        public List<Post> GetAllPosts()
        {
            return PostRepository.GetAllPosts();
        }

        public List<Post> GetPostsByForumAndUser(int forumId,int userId)
        {
            return PostRepository.GetPostsByForumAndUser(forumId, userId);
        }

        public List<Post> GetPostsByForumNameAndUserName(string forumName, string userName)
        {
            return PostRepository.GetPostsByForumNameAndUserName(forumName, userName);
        }

        public bool CreatePost(int forumId, int userId, string postText)
        {
            return PostRepository.CreatePost(forumId, userId, postText);
        }

        public bool UpdatePost(int chosenPostId, string newPostText)
        {
            return PostRepository.UpdatePost(chosenPostId, newPostText);
        }

        public bool DeletePost(int chosenPostId)
        {
        return PostRepository.DeletePost(chosenPostId);
        }
    }
}
