namespace Forum.App.Commands
{
    using Contracts;

    public class LoginMenuCommand : ICommand
    {
        private IMenuFactory menuFactory;

        public LoginMenuCommand(IMenuFactory menuFactory)
        {
            this.menuFactory = menuFactory;
        }

        public IMenu Execute(params string[] args)
        {
            var commandName = this.GetType().Name;
            var menuName = commandName.Substring(0, commandName.Length - "Command".Length);

            var menu = menuFactory.CreateMenu(menuName);
            return menu;
        }
    }
}
