namespace _03BarracksFactory.Core.Commands
{
    using Contracts;
    using Attributes;

    public class RetireCommand : Command
    {
        [Inject]
        IRepository repository;

        public RetireCommand(string[] data, IRepository repository)
            : base(data)
        {
            this.Repository = repository;
        }

        public IRepository Repository
        {
            get { return this.repository; }

            private set { this.repository = value; }
        }

        public override string Execute()
        {
            var unitType = this.Data[1];
            this.Repository.RemoveUnit(unitType);

            return unitType + " retired!";
        }
    }
}
