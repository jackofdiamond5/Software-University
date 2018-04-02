namespace _03BarracksFactory.Core
{
    using System;
    using System.Reflection;

    using Contracts;

    class Engine : IRunnable
    {
        private ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] data = input.Split();
                    string commandName = data[0];

                    var commandInstance = commandInterpreter.InterpretCommand(data, commandName);
                    var method = commandInstance
                        .GetType()
                        .GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public);

                    try
                    {
                        var result = (string)method.Invoke(commandInstance, null);
                        Console.WriteLine(result);
                    }
                    catch (TargetInvocationException ex)
                    {
                        throw ex.InnerException;
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
