using System;
using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("display")]
    public class DisplayCommand : Command
    {
        private string input;
        private string[] data;

        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
        {
            this.input = input;
            this.data = data;
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            var entityToDisplay = this.Data[1];
            var sortType = this.Data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                var studentsComparator = this.CreateStudentComparator(sortType);
                var list = this.repository.GetAllStudentsSorted(studentsComparator);

                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var coursesComparator = this.CreateCourseComparator(sortType);
                var list = this.repository.GetAllCoursesSorted(coursesComparator);

                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }

        private IComparer<IStudent> CreateStudentComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((sOne, sTwo) => sOne.CompareTo(sTwo));
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((sOne, sTwo) => sTwo.CompareTo(sOne));
            }

            throw new InvalidCommandException(sortType);
        }

        private IComparer<ICourse> CreateCourseComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((cOne, cTwo) => cOne.CompareTo(cTwo));
            }
            else if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((cOne, cTwo) => cTwo.CompareTo(cOne));
            }
            else
            {
                throw new InvalidCommandException(sortType);
            }
        }
    }
}