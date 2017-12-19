using System;
using System.Linq;
using EmployeesMapping.Data;

namespace EmployeesMapping
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            InitializeDb();
            var commandParser = new CommandParser(serviceProvider);

            while (true)
            {
                try
                {
                    Console.Write("Enter a command: ");

                    var input = Console.ReadLine().Trim();

                    var data = input.Split(' ');
                    var commandName = data.First();
                    var commandArgs = data.Skip(1).ToArray();

                    var command = commandParser.ParseCommand(commandName);

                    var result = command.Execute(commandArgs);

                    if (result.Equals("Exit"))
                    {
                        Environment.Exit(0);
                    }

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void InitializeDb()
        {
            var context = new EmployeesContext();
            context.Database.EnsureCreated();
        }
    }
}
