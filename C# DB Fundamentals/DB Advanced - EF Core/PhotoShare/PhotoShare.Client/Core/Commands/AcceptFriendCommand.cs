namespace PhotoShare.Client.Core.Commands
{
    using Contracts;
    using Services.Contracts;

    using System;

    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(params string[] data)
        {
            var firstUsername = data[0];
            var secondUsername = data[1];

            userService.AceptFriend(firstUsername, secondUsername);

            return $"{firstUsername} accepted {secondUsername} as a friend";
        }
    }
}
