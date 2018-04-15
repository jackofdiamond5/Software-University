using System;
using System.Linq;
using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.StaticData;

namespace BashSoft.Models
{
    public class Student : IStudent
    {
        private string userName;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.UserName = userName;
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses => this.enrolledCourses;

        public IReadOnlyDictionary<string, double> MarksByCourseName => this.marksByCourseName;

        public int CompareTo(IStudent other)
        {
            return this.UserName.CompareTo(other.UserName);
        } 
        
        public void EnrollInCourse(ICourse course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.UserName, course.Name);
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException();
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException(nameof(scores), ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        public override string ToString()
        {
            return this.UserName;
        }

        private double CalculateMark(int[] scores)
        {
            var percentageOfSolvedExams =
                scores.Sum() / (double)(Course.NumberOfTasksOnExam * Course.MaxScoreExamTask);

            var mark = percentageOfSolvedExams * 4 + 2;
            return mark;
        }
    }
}
