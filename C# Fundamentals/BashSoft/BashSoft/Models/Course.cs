﻿using BashSoft.IO;
using BashSoft.Static_data;
using System.Collections.Generic;

namespace BashSoft.Models
{
    public class Course
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreExamTask = 100;

        public string name;
        public Dictionary<string, Student> studentsByName;

        public Course(string name)
        {
            this.name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

        public void EnrollStudent(Student student)
        {
            if (this.studentsByName.ContainsKey(student.username))
            {
                OutputWriter.DisplayException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    student.username, this.name));

                return;
            }

            this.studentsByName.Add(student.username, student);
        }
    }
}