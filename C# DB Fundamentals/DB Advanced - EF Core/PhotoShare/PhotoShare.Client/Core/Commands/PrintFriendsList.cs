namespace PhotoShare.Client.Core.Commands
{
    using Contracts;
    using Services.Contracts;

    public class PrintFriendsListCommand : ICommand
    {
        private readonly IUserService userService;

        public PrintFriendsListCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(params string[] data)
        {
            var username = data[0];

            var friendList = userService.ListFriends(username);

            return friendList;
        }
    }
}
