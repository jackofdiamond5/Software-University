using System.Xml;

namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Data;
    using Models;
    using Contracts;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public User ById(int id)
        {
            var user = context.Users.Find(id);

            if (user is null)
            {
                throw new ArgumentException($"UserId {id} not found!");
            }

            return user;
        }

        public User ByUserName(string username)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username.Equals(username));

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            return user;
        }

        public User ByUserNameAndPassword(string username, string password)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));

            if (user is null || !user.Password.Equals(password))
            {
                throw new ArgumentException("Invalid username or password!");
            }

            return user;
        }

        public User Create(string username, string password, string email)
        {
            var user = new User(username, password, email);

            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username.Equals(username));

            if (user is null)
            {
                throw new InvalidOperationException("User does not exist!");
            }

            context.Users.Find(user.Id).IsDeleted = true;

            context.SaveChanges();
        }

        public User ModifyPassword(string username, string newPassword)
        {
            var user = ByUserName(username);

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!Regex.IsMatch(newPassword, "[a-z]") || !Regex.IsMatch(newPassword, "\\d"))
            {
                throw new ArgumentException("Invalid Password");
            }

            user.Password = newPassword;

            context.SaveChanges();

            return user;
        }

        public User ModifyBorntown(string username, string newBornTown)
        {
            var user = ByUserName(username);

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var towns = context.Towns
                .Select(t => t.Name)
                .ToArray();

            if (!towns.Contains(newBornTown))
            {
                throw new ArgumentException($"Town {newBornTown} not found!");
            }

            var userTown = user.BornTown;

            if (userTown.Name.Equals(newBornTown))
            {
                throw new ArgumentException("Town names cannot be the same!");
            }

            userTown.Name = newBornTown;

            context.SaveChanges();

            return user;
        }

        public User ModifyCurrentTown(string username, string newCurrentTown)
        {
            var user = ByUserName(username);

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var towns = context.Towns
                .Select(t => t.Name)
                .ToArray();

            if (!towns.Contains(newCurrentTown))
            {
                throw new ArgumentException($"Town {newCurrentTown} not found!");
            }

            var userTown = user.CurrentTown;

            if (userTown.Name.Equals(newCurrentTown))
            {
                throw new ArgumentException("Town names cannot be the same!");
            }

            userTown.Name = newCurrentTown;

            context.SaveChanges();

            return user;
        }

        public void AddFriend(string firstUsername, string secondUsername)
        {
            var firstUser = context.Users
                .SingleOrDefault(u => u.Username == firstUsername);

            var secondUser = context.Users
                .SingleOrDefault(u => u.Username == secondUsername);

            if (firstUser is null || secondUser is null)
            {
                var notExisting = firstUser is null ? firstUsername : secondUsername;
                throw new ArgumentException($"{notExisting} not found!");
            }

            var friendship = new Friendship
            {
                UserId = firstUser.Id,
                User = firstUser,
                FriendId = secondUser.Id,
                Friend = secondUser
            };

            var friendshipExists = context.Friendships
                .Any(f => f.Equals(friendship));

            if (friendshipExists)
            {
                throw new InvalidOperationException($"{secondUsername} is already a friend to {firstUsername}");
            }

            context.Friendships.Add(friendship);

            context.SaveChanges();
        }

        public void AceptFriend(string firstUsername, string secondUsername)
        {
            var firstUser = context.Users
                .SingleOrDefault(u => u.Username == firstUsername);

            var secondUser = context.Users
                .SingleOrDefault(u => u.Username == secondUsername);

            if (firstUser is null || secondUser is null)
            {
                var notExisting = firstUser is null ? firstUsername : secondUsername;
                throw new ArgumentException($"{notExisting} not found!");
            }

            var friendship = context.Friendships
                .SingleOrDefault(f => f.UserId == firstUser.Id && f.FriendId == secondUser.Id);

            var friendRequest = context.Friendships
                .SingleOrDefault(f => f.UserId == secondUser.Id && f.FriendId == firstUser.Id);

            if (friendship != null && friendRequest != null)
            {
                throw new InvalidOperationException($"{secondUsername} is already a friend to {firstUsername}");
            }

            if (friendRequest == null)
            {
                throw new ArgumentException($"{secondUsername} has not added {firstUsername} as a friend");
            }

            AddFriend(firstUsername, secondUsername);
        }

        public string ListFriends(string username)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username == username);

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var builder = new StringBuilder();

            builder.AppendLine("Friends:");

            var friendships = context.Friendships
                .Select(f => new
                {
                    f.UserId,
                    f.Friend
                })
                .Where(f => f.UserId == user.Id)
                .ToArray();

            if (friendships.Length == 0)
            {
                return "No friends for this user. :(";
            }

            foreach (var friend in friendships)
            {
                builder.AppendLine($"-{friend.Friend.Username}");
            }

            return builder.ToString();
        }
    }
}
