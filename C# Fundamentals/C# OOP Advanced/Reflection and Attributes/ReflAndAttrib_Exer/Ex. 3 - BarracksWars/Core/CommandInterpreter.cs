namespace _03BarracksFactory.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Contracts;
    using Attributes;

    public class CommandInterpreter : ICommandInterpreter
    {
        IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            IExecutable commandInstance;
            try
            {
                var commandNameUpper = commandName.ToUpper();
                var commandFullName = commandNameUpper[0] + string.Join("", commandNameUpper.ToLower().Skip(1));
                var commandType = Type.GetType($"_03BarracksFactory.Core.Commands.{commandFullName}Command");

                var fields = commandType
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f => f.CustomAttributes.Any(ca => ca.AttributeType.Equals(typeof(InjectAttribute))))
                    .ToArray();

                var injectArgs = fields
                    .Select(f => serviceProvider.GetService(f.FieldType))
                    .ToArray();

                var allArgs = new object[] { data }.Concat(injectArgs).ToArray();

                commandInstance = (IExecutable)Activator.CreateInstance(commandType, allArgs);
            }
            catch (NullReferenceException)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            return commandInstance;
        }
    }
}
