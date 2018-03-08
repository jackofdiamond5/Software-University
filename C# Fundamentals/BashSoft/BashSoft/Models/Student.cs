using System;
using System.Linq;
using System.Collections.Generic;

using BashSoft.Static_data;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
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
                    throw new ArgumentNullException(nameof(this.userName), ExceptionMessages.NullOrEmptyValue);
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses => this.enrolledCourses;
        
        public IReadOnlyDictionary<string, double> MarksByCourseName => this.marksByCourseName;


        public void EnrollInCourse(Course course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new ArgumentException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    this.UserName, course.Name));
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new ArgumentNullException(nameof(courseName), ExceptionMessages.NotEnrolledInCourse);
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException(nameof(scores), ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
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
