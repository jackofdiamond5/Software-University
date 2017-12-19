
namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Models;
    using Services;
    using Contracts;
    using Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;

        public ModifyUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(params string[] data)
        {
            var username = data[0];
            var property = data[1];
            var value = data[2];

            if (Session.User is null || !Session.User.Username.Equals(username))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            var propertyInfo = typeof(User).GetProperties()
                .SingleOrDefault(pi =>
                string.Equals(pi.Name.Trim(), property.Trim(), StringComparison.CurrentCultureIgnoreCase));

            if (propertyInfo is null)
            {
                throw new ArgumentException($"Property {property.Trim()} not found!");
            }

            typeof(UserService).GetMethods()
                .Single(pi => pi.Name.Equals($"Modify{property}"))
                .Invoke(userService, new object[] { username, value });

            return $"User {username} {property} is {value}.";
        }
    }
}
