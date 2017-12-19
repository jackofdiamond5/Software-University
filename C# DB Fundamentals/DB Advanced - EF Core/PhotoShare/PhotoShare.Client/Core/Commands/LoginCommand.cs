namespace PhotoShare.Client.Core.Commands
{

    using System;

    using Contracts;
    using Services.Contracts;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] data)
        {
            var username = data[0];
            var password = data[1];

            var user = userService.ByUserNameAndPassword(username, password);

            if (Session.User != null)
            {
                throw new ArgumentException("You should logout first!");
            }

            Session.User = user;

            return $"User {username} successfully logged in!";
        }
    }
}
