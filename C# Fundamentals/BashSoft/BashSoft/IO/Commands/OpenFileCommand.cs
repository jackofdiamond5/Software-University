using System.Diagnostics;

using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.Exceptions;
using BashSoft.Static_data;

namespace BashSoft.IO.Commands
{
    class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {

        }

        public override void Execute()
        {
            if(this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            var fileName = this.Data[1];
            Process.Start(SessionData.CurrentPath + "\\" + fileName);
        }
    }
}
