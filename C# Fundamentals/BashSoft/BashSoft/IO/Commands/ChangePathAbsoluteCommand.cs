using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO
{
    internal class ChangePathAbsoluteCommand : Command
    {
        public ChangePathAbsoluteCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IoManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager) { }
        
        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                var absolutePath = this.Data[1];
                this.InputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
        }
    }
}