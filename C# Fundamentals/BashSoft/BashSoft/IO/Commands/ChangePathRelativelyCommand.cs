using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("cdrel")]
    internal class ChangePathRelativelyCommand : Command
    {
        [Inject]
        private IDirectoryChanger inputOutputManager;

        public ChangePathRelativelyCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            var relPath = this.Data[1];
            this.inputOutputManager.ChangeCurrentDirectoryAbsolute(relPath);
        }
    }
}