using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.StaticData;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("ls")]
    internal class TraverseFolderCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputManager;

        public TraverseFolderCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 1:
                    this.inputOutputManager.TraverseDirectory(0);
                    break;
                case 2:
                    var canBeParsed = int.TryParse(this.Data[1], out int depth);

                    if (canBeParsed)
                    {
                        this.inputOutputManager.TraverseDirectory(depth);
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