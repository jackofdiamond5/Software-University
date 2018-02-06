using System;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool IsDataInitialized;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData(string fileName)
        {
            if (!IsDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
                IsDataInitialized = true;
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        private static void ReadData(string fileName)
        {
            var path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var allInputLines = File.ReadAllLines(path);

                for (var line = 0; line < allInputLines.Length; line++)
                {
                    if (string.IsNullOrEmpty(allInputLines[line]))
                        continue;

                    var tokens = allInputLines[line].Split();

                    var course = tokens[0];
                    var student = tokens[1];
                    var mark = int.Parse(tokens[2]);

                    if (!studentsByCourse.ContainsKey(course))
                    {
                        studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                    }
                    else
                    {
                        if (!studentsByCourse[course].ContainsKey(student))
                        {
                            studentsByCourse[course].Add(student, new List<int>());
                        }

                        studentsByCourse[course][student].Add(mark);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (!studentsByCourse.ContainsKey(courseName))
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

        private static bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
            {
                return true;
            }

            OutputWriter.DisplayException(ExceptionMessages.IndexistingStudentInDataBase);

            return false;
        }

        public static void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(
                    new KeyValuePair<string, List<int>>(username, studentsByCourse[courseName][username]));
            }
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (!IsQueryForCoursePossible(courseName))
                return;

            OutputWriter.WriteMessageOnNewLine($"{courseName}");
            OutputWriter.WriteMessageOnNewLine("");

            foreach (var courseEntry in studentsByCourse)
            {
                var currCourseEntry = courseEntry.Value;
                foreach (var studentMarksEntry in currCourseEntry)
                {
                    OutputWriter.PrintStudent(
                        new KeyValuePair<string, List<int>>(studentMarksEntry.Key, studentMarksEntry.Value));
                }
            }
        }
    }
}
