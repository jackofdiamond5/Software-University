using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    internal class TraverseFolderCommand : Command
    {
        public TraverseFolderCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IoManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager) { }
        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 1:
                    this.InputOutputManager.TraverseDirectory(0);
                    break;
                case 2:
                    var canBeParsed = int.TryParse(this.Data[1], out int depth);

                    if (canBeParsed)
                    {
                        this.InputOutputManager.TraverseDirectory(depth);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                    }
                    break;
                default:
                    return;
            }
        }
    }
}