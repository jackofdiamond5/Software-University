using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    internal class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, Tester judge,
            StudentsRepository repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager) { }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);
            }

            this.Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }
}