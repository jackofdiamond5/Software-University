namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(params string[] data)
        {
            var username = data[0];
            var password = data[1];
            var repeatPassword = data[2];
            var email = data[3];

            if (Session.User != null)
            {
                throw new ArgumentException("You are already logged in!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            var userExists = userService.ByUserName(username);

            if (userExists != null)
            {
                throw new InvalidOperationException($"Username {username} already taken!");
            }

            userService.Create(username, password, email);
            
            return $"User {username} was registered successfully!";
        }
    }
}
