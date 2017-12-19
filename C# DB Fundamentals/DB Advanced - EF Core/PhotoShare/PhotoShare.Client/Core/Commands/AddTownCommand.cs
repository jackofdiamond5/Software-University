namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;

        public AddTownCommand(ITownService townService)
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(params string[] data)
        {
            if (Session.User is null)
            {
                throw new ArgumentException("Invalid credentials!");
            }

            var townName = data[0];
            var country = data[1];

            townService.AddTown(townName, country);

            return $"{townName} was added to database!";
        }
    }
}