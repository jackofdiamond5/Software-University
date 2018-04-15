using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    internal class ReadDatabaseFromFileCommand : Command
    {
        public ReadDatabaseFromFileCommand(string input, string[] data, Tester judge,
            StudentsRepository repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager) { }

        public override void Execute()
        {
            var fileName = this.Data[1];
            this.Repository.LoadData(fileName);
        }
    }
}