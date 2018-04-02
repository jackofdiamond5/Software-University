namespace _03BarracksFactory.Core.Commands
{
    using Contracts;
    using Attributes;

    public class AddCommand : Command
    {
        [Inject]
        IRepository repository;
        [Inject]
        IUnitFactory unitFactory;
        
        public AddCommand(string[] data, IRepository repository, IUnitFactory unitFactory) 
            : base(data)
        {
            this.Repository = repository;
            this.UnitFactory = unitFactory;
        }

        public IRepository Repository
        {
            get { return this.repository; }

            private set { this.repository = value; }
        }

        public IUnitFactory UnitFactory
        {
            get { return this.unitFactory; }

            private set { this.unitFactory = value; }
        }

        public override string Execute()
        {
            string unitType = this.Data[1];
            IUnit unitToAdd = this.UnitFactory.CreateUnit(unitType);
            this.Repository.AddUnit(unitToAdd);
            string output = unitType + " added!";
            return output;
        }
    }
}
