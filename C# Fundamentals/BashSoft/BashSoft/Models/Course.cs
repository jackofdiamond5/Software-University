using System;
using System.Collections.Generic;

using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Models
{
    public class Course
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreExamTask = 100;

        private string name;
        private Dictionary<string, Student> studentsByName;

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
                    throw new ArgumentNullException(nameof(this.name), ExceptionMessages.NullOrEmptyValue);
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, Student> StudentsByName => studentsByName;

        public void EnrollStudent(Student student)
        {
            if (this.StudentsByName.ContainsKey(student.UserName))
            {
                throw new ArgumentException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    student.UserName, this.Name));
            }

            this.studentsByName.Add(student.UserName, student);
        }
    }
}