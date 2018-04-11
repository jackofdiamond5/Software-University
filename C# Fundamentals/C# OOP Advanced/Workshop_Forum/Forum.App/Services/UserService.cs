namespace Forum.App.Services
{
    using Forum.Data;
    using Forum.DataModels;
    using Forum.App.Contracts;

    public class UserService : IUserService
    {
        private ForumData forumData;
        private ISession session;

        public UserService(ForumData forumData, ISession session)
        {
            this.forumData = forumData;
            this.session = session;
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
