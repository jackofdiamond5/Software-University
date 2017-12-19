using System;

namespace PhotoShare.Client.Core.Commands
{

    using Contracts;
    //using Services.Contracts;

    public class LogoutCommand : ICommand
    {
        //private readonly IUserService userService;

        //public LogoutCommand(IUserService userService)
        //{
        //    this.userService = userService;
        //}

        public string Execute(params string[] data)
        {
            var username = data[0];

            if (Session.User is null)
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }

            if (!Session.User.Username.Equals(username))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            Session.User = null;

            return $"User {username} successfully logged out!";
        }
    }
}
