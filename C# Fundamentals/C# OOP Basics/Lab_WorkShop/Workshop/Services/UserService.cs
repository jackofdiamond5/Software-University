namespace Forum.App.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using Forum.Data;
    using Forum.Models;
    using Forum.App.Controllers;

    public class UserService
    {
        public static bool TryLogInUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var forumData = new ForumData();

            var userExists = forumData.Users.Any(u => u.Username == username && u.Password == password);
            return userExists;
        }

        public static SignUpStatus TrySignUpUser(string username, string password)
        {
            var validUsername = !string.IsNullOrWhiteSpace(username) && username.Length > 3;
            var validPassword = !string.IsNullOrWhiteSpace(password) && password.Length > 3;

            if(!validUsername || !validPassword)
            {
                return SignUpStatus.DetailsError;
            }

            var forumData = new ForumData();

            var userAlreadyExists = forumData.Users.Any(u => u.Username == username);

            if (!userAlreadyExists)
            {
                var userId = forumData.Users.LastOrDefault()?.Id + 1 ?? 1;
                var user = new User(userId, username, password, new List<int>());
                forumData.Users.Add(user);
                forumData.SaveChanges();

                return SignUpStatus.Success;
            }

            return SignUpStatus.UsernameTakenError;
        }
    }
}
