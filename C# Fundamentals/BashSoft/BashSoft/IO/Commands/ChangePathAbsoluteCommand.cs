using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("cdabs")]
    internal class ChangePathAbsoluteCommand : Command
    {
        [Inject]
        private IDirectoryChanger inputOutputManager;

        public ChangePathAbsoluteCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            var absolutePath = this.Data[1];
            this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    }
}