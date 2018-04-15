using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.IO.Commands;

namespace BashSoft.IO
{
    internal class ShowWantedDataCommand : Command
    {
        public ShowWantedDataCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IDirectoryManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 2:
                    {
                        var courseName = this.Data[1];
                        this.Repository.GetAllStudentsFromCourse(courseName);
                    }
                    break;
                case 3:
                    {
                        var courseName = this.Data[1];
                        var userName = this.Data[2];
                        this.Repository.GetStudentScoresFromCourse(courseName, userName);
                    }
                    break;
            }
        }
    }
}