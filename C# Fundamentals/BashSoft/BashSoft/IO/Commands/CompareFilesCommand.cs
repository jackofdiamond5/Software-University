using BashSoft.Judge;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("cmp")]
    internal class CompareFilesCommand : Command
    {
        [Inject]
        private Tester judge;

        public CompareFilesCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if (this.Data.Length == 3)
            {
                var firstPath = this.Data[1];
                var secondPath = this.Data[2];

                this.judge.CompareContent(firstPath, secondPath);
            }
        }
    }
}