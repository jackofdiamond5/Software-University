using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    internal class ChangePathRelativelyCommand : Command
    {
        public ChangePathRelativelyCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager) { }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                var relPath = this.Data[1];
                this.InputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
        }
    }
}