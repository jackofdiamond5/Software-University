namespace PhotoShare.Services
{
    using System;

    using Data;
    using Contracts;

    public class ApplicationInterfaceService : IApplicationInterfaceService
    {
        private readonly PhotoShareContext context;

        public ApplicationInterfaceService(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Exit(string command)
        {
            if (command.Equals("Exit"))
            {
                return "Bye-bye!";
            }

            throw new ArgumentException("Invalid command!");
        }
    }
}
