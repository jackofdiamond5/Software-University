namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;
    
    public class DeleteUser : ICommand
    {
        private readonly IUserService userService;

        public DeleteUser(IUserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(params string[] data)
        {
            var username = data[1];

            if (Session.User is null || Session.User.Username != username)
            {
                throw new ArgumentException("Invalid credentials!");
            }

            userService.Delete(username);

            return $"User {username} was deleted from the database!";
        }
    }
}
