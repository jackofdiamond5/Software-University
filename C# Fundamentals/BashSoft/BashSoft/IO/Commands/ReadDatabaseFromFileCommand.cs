using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO
{
    internal class ReadDatabaseFromFileCommand : Command
    {
        public ReadDatabaseFromFileCommand(string input, string[] data, Tester judge,
            StudentsRepository repository, IoManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager) { }

        public override void Execute()
        {
            var fileName = this.Data[1];
            this.Repository.LoadData(fileName);
        }
    }
}