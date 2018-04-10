namespace Forum.App.Factories
{
    using Contracts;

    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandFactory : ICommandFactory
    {
        IServiceProvider serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var commandType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            if (!typeof(ICommand).IsInstanceOfType(commandType))
            {
                throw new InvalidOperationException($"{commandName} is not a command!");
            }

            var ctorParams = commandType.GetConstructors().First().GetParameters();
            var args = new object[] { ctorParams.Length };

            for(var i = 0; i < args.Length; i++)
            {
                args[i] = this.serviceProvider.GetService(ctorParams[i].ParameterType);
            }

            var command = (ICommand)Activator.CreateInstance(commandType, args);

            return command;
        }
    }
}
