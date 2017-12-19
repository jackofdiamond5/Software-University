using System.Linq;
using PhotoShare.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoShare.Client.Core
{
    using System;

    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var dbInitializeService = serviceProvider.GetService<IDatabaseInitializerService>();
            dbInitializeService.InitializeDatabase();

            var commandDispatcher = new CommandDispatcher(serviceProvider);

            while (true)
            {
                try
                {
                    Console.Write("Enter a command: ");

                    var input = Console.ReadLine().Trim();

                    var data = input.Split(' ');
                    var commandName = data.First();
                    var commandArgs = data.Length == 1 ?
                        data.Take(1).ToArray() 
                        : data.Skip(1).ToArray();

                    var command = commandDispatcher.ParseCommand(commandName);

                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);

                    if (result.Equals("Bye-bye!"))
                    {
                        Environment.Exit(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
