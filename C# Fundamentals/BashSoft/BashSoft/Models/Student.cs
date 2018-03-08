using System.Linq;
using System.Collections.Generic;

using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Models
{
    public class Student
    {
        public string username;
        public Dictionary<string, Course> enrolledCorses;
        public Dictionary<string, double> marksByCourseName;

        public Student(string username)
        {
            this.username = username;
            this.enrolledCorses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCorses.ContainsKey(course.name))
            {
                OutputWriter.DisplayException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    this.username, course.name));

                return;
            }

            this.enrolledCorses.Add(course.name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCorses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if(scores.Length > Course.NumberOfTasksOnExam)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                return;
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
