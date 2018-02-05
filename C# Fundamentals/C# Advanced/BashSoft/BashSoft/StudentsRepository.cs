using System;
using System.Collections.Generic;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData()
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData();
                isDataInitialized = true;
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        private static void ReadData()
        {
            string input;

            while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            {
                var tokens = input.Split(' ');
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

            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (!studentsByCourse.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
                return false;
            }

            if (isDataInitialized)
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
