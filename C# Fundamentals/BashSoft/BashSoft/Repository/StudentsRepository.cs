using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using BashSoft.IO;
using BashSoft.Models;
using BashSoft.Static_data;
using System;
using System.Linq;

namespace BashSoft.Repository
{
    public class StudentsRepository
    {
        //private Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public bool IsDataInitialized;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;
        private RepositoryFilter filter;
        private RepositorySorter sorter;

        public StudentsRepository(RepositoryFilter filter, RepositorySorter sorter)
        {
            if (this.IsDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitializedException);
                return;
            }

            this.filter = filter;
            this.sorter = sorter;
        }

        public void UnloadData()
        {
            if (!this.IsDataInitialized)
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedException);
            }

            this.students = null;
            this.courses = null;
            this.IsDataInitialized = false;
        }

        public void LoadData(string fileName)
        {
            if (this.IsDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
                return;
            }

            OutputWriter.WriteMessageOnNewLine("Reading data...");
            this.students = new Dictionary<string, Student>();
            this.courses = new Dictionary<string, Course>();
            ReadData(fileName);
            IsDataInitialized = true;
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName))
                return;

            if (studentsToTake == null)
            {
                studentsToTake = this.courses[courseName].studentsByName.Count;
            }

            var marks = this.courses[courseName].studentsByName
                .ToDictionary(s => s.Key, s => s.Value.marksByCourseName[courseName]);

            this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].studentsByName.Count;
                }

                var marks = this.courses[courseName].studentsByName
                    .ToDictionary(c => c.Key, c => c.Value.marksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        private void ReadData(string fileName)
        {
            var path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var regex =
                    new Regex(@"([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)");
                var allInputLines = File.ReadAllLines(path);

                for (var line = 0; line < allInputLines.Length; line++)
                {
                    if (string.IsNullOrEmpty(allInputLines[line]) || !regex.IsMatch(allInputLines[line]))
                        continue;

                    try
                    {
                        var currentMatch = regex.Match(allInputLines[line]);
                        var courseName = currentMatch.Groups[1].Value;
                        var username = currentMatch.Groups[2].Value;
                        var scoresStr = currentMatch.Groups[3].Value;

                        var scores = scoresStr
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

                        if (scores.Any(s => s > 100 || s < 0))
                        {
                            OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                        }

                        if (scores.Length > Course.NumberOfTasksOnExam)
                        {
                            OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                            continue;
                        }

                        if (!this.students.ContainsKey(username))
                        {
                            this.students.Add(username, new Student(username));
                        }

                        if (!this.courses.ContainsKey(courseName))
                        {
                            this.courses.Add(courseName, new Course(courseName));
                        }

                        var course = this.courses[courseName];
                        var student = this.students[username];

                        student.EnrollInCourse(course);
                        student.SetMarkOnCourse(courseName, scores);

                        course.EnrollStudent(student);
                    }
                    catch (FormatException fex)
                    {
                        OutputWriter.DisplayException($"{fex.Message} at line: {line}");
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (!this.courses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                return false;
            }

            if (IsDataInitialized)
            {
                return true;
            }

            OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedException);
            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].studentsByName.ContainsKey(studentUserName))
            {
                return true;
            }

            OutputWriter.DisplayException(ExceptionMessages.IndexistingStudentInDataBase);

            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(
                    new KeyValuePair<string, double>(username, 
                    this.courses[courseName].studentsByName[username].marksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (!IsQueryForCoursePossible(courseName))
                return;

            OutputWriter.WriteMessageOnNewLine($"{courseName}");
            OutputWriter.WriteMessageOnNewLine("");

            foreach (var courseEntry in this.courses[courseName].studentsByName)
            {
                var currCourseEntry = courseEntry.Value;
                foreach (var studentMarksEntry in this.courses[courseName].studentsByName)
                {
                    GetStudentScoresFromCourse(courseName, studentMarksEntry.Key);
                }
            }
        }
    }
}
