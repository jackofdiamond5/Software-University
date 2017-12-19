namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;
    
    public class AddFriendCommand : ICommand    
    {
        private readonly IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(params string[] data)
        {
            var firstUsername = data[0];
            var secondUsername = data[1];

            if (Session.User is null || !Session.User.Username.Equals(firstUsername))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            userService.AddFriend(firstUsername, secondUsername);

            return $"Friend {secondUsername} added to {firstUsername}";
        }
    }
}
