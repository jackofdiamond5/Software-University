using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("readdb")]
    internal class ReadDatabaseFromFileCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ReadDatabaseFromFileCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            var fileName = this.Data[1];
            this.repository.LoadData(fileName);
        }
    }
}