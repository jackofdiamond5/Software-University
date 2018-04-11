namespace Forum.App.Services
{
    using System.Collections.Generic;

    using Forum.Data;
    using Forum.DataModels;
    using Forum.App.Contracts;

    public class PostService : IPostService
    {
        private ForumData forumData;
        private ISession session;

        public PostService(ForumData forumData, ISession session)
        {
            this.forumData = forumData;
            this.session = session;
        }

        public int AddPost(int userId, string postTitle, string postCategory, string postContent)
        {
            throw new System.NotImplementedException();
        }

        public void AddReplyToPost(int postId, string replyContents, int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategoryInfoViewModel> GetAllCategories()
        {
            throw new System.NotImplementedException();
        }

        public string GetCategoryName(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IPostInfoViewModel> GetCategoryPostsInfo(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IPostViewModel GetPostViewModel(int postId)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserName(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLogInUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool TrySignUpUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
