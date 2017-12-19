namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Commands.Contracts;

    public class CommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType is null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var command = InjectServices(commandType);

            return command;
        }

        private ICommand InjectServices(Type type)
        {
            var ctor = type.GetConstructors().First();

            var ctorParams = ctor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();

            var services = ctorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)ctor.Invoke(services);

            return command;
        }
    }
}
