namespace Forum.App.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Forum.App.Contracts;

    public class MenuFactory : IMenuFactory
    {
        IServiceProvider serviceProvider;

        public MenuFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMenu CreateMenu(string menuName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var menuType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{menuName}Menu");

            if(menuType == null)
            {
                throw new InvalidOperationException("Menu not found!");
            }

            var ctorParams = menuType.GetConstructors().First().GetParameters();
            var args = new object[] { ctorParams.Length };

            for(var i = 0; i < args.Length; i++)
            {
                args[i] = this.serviceProvider.GetService(ctorParams[i].ParameterType);
            }

            var menu = (IMenu)Activator.CreateInstance(menuType, args);

            return menu;
        }
    }
}
