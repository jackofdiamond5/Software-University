namespace Forum.App.Services
{
    using System;
    using System.Linq;

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

            var user = this.forumData.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with username: \"{user.Username}\" does not exist!");
            }

            return user;
        }

        public string GetUserName(int userId)
        {
            var user = this.forumData.Users.FirstOrDefault(u => u.Id == userId);

            if(user == null)
            {
                throw new InvalidOperationException($"User with username: \"{user.Username}\" does not exist!");
            }

            return user.Username;
        }

        public bool TryLogInUser(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = this.forumData.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if(user == null)
            {
                return false;
            }

            session.Reset();
            session.LogIn(user);

            return true;
        }

        public bool TrySignUpUser(string username, string password)
        {
            var validUsername = !string.IsNullOrWhiteSpace(username) && username.Length > 3;
            var validPassword = !string.IsNullOrWhiteSpace(password) && password.Length > 3;

            if (!validUsername || !validPassword)
            {
                throw new ArgumentException("Username and Password must be longer than 3 symbols!");
            }

            var userAlreadyExists = forumData.Users.Any(u => u.Username == username);
            if (userAlreadyExists)
            {
                throw new InvalidOperationException("Username taken!");
            }

            var userId = forumData.Users.LastOrDefault()?.Id + 1 ?? 1;
            var user = new User(userId, username, password);

            forumData.Users.Add(user);
            forumData.SaveChanges();

            this.TryLogInUser(username, password);

            return true;
        }
    }
}
