using System;
using System.Collections.Generic;

using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public class DisplayCommand : Command
    {
        private string input;
        private string[] data;
        private Tester judge;
        private StudentsRepository repository;
        private IDirectoryManager inputOutputManager;

        public DisplayCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IDirectoryManager inputOutputManager)
            : base(input, data, judge, repository, inputOutputManager)
        {
            this.input = input;
            this.data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public override void Execute()
        {
            if(this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
            }

            var entityToDisplay = this.Data[1];
            var sortType = this.Data[2];

            if(entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                var studentsComparator = this.CreateStudentComparator(sortType);
                var list = this.Repository.GetAllStudentsSorted(studentsComparator);

                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if(entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var coursesComparator = this.CreateCourseComparator(sortType);
                var list = this.Repository.GetAllCoursesSorted(coursesComparator);

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
            else if(sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((sOne, sTwo) => sTwo.CompareTo(sOne));
            }

            throw new InvalidCommandException(sortType);
        }

        private IComparer<ICourse> CreateCourseComparator(string sortType)
        {
            if(sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((cOne, cTwo) => cOne.CompareTo(cTwo));
            }
            else if(sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
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