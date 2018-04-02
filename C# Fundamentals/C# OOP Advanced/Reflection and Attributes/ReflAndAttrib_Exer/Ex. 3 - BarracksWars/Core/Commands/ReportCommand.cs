namespace _03BarracksFactory.Core.Commands
{
    using Contracts;
    using Attributes;

    public class ReportCommand : Command
    {
        [Inject]
        IRepository repository;

        public ReportCommand(string[] data, IRepository repository) 
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
            string output = this.Repository.Statistics;
            return output;
        }
    }
}
