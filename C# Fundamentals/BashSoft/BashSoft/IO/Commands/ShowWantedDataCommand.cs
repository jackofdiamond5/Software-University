using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    [Alias("show")]
    internal class ShowWantedDataCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ShowWantedDataCommand(string input, string[] data) 
            : base(input, data) { }
        
        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 2:
                    {
                        var courseName = this.Data[1];
                        this.repository.GetAllStudentsFromCourse(courseName);
                    }
                    break;
                case 3:
                    {
                        var courseName = this.Data[1];
                        var userName = this.Data[2];
                        this.repository.GetStudentScoresFromCourse(courseName, userName);
                    }
                    break;
            }
        }
    }
}