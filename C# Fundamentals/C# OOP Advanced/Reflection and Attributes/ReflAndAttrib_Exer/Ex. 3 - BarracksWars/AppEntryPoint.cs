namespace _03BarracksFactory
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Data;
    using Contracts;
    using Core.Factories;
    
    class AppEntryPoint
    {
        static void Main(string[] args)
        {
            var serviceProvider = GetServices();
            ICommandInterpreter commandInterpreter = new CommandInterpreter(serviceProvider);
            IRunnable engine = new Engine(commandInterpreter);
            engine.Run();
        }

        private static IServiceProvider GetServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRepository, UnitRepository>();
            serviceCollection.AddTransient<IUnitFactory, UnitFactory>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
