namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;

    public class ExitCommand : ICommand
    {
        private readonly IApplicationInterfaceService appService;

        public ExitCommand(IApplicationInterfaceService appService)
        {
            this.appService = appService;
        }

        public string Execute(params string[] data)
        {
            var command = data[0];

            var result = appService.Exit(command);

            return result;
        }
    }
}
