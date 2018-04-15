using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Course : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreExamTask = 100;

        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public Course(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName => studentsByName;

        public int CompareTo(ICourse other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public void EnrollStudent(IStudent student)
        {
            if (this.StudentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, this.Name);
            }

            this.studentsByName.Add(student.UserName, student);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}