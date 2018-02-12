using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
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

        public static void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName))
                return;

            if (studentsToTake == null)
            {
                studentsToTake = studentsByCourse[courseName].Count;
            }

            RepositorySorters.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);
        }
        
        public static void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                RepositoryFilters.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);
            }
        }

        private static void ReadData(string fileName)
        {
            var path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var regex = new Regex(@"([A-Z][a-zA-Z#+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)");
                var allInputLines = File.ReadAllLines(path);

                for (var line = 0; line < allInputLines.Length; line++)
                {
                    if (string.IsNullOrEmpty(allInputLines[line]) || !regex.IsMatch(allInputLines[line]))
                        continue;

                    var currentMatch = regex.Match(allInputLines[line]);
                    var courseName = currentMatch.Groups[1].Value;
                    var username = currentMatch.Groups[2].Value;
                    var hasParsedScore = int.TryParse(currentMatch.Groups[3].Value, out int studentScoreOnTask);

                    if (hasParsedScore && studentScoreOnTask >= 0 && studentScoreOnTask <= 100)
                    {
                        if (!studentsByCourse.ContainsKey(courseName))
                        {
                            studentsByCourse.Add(courseName, new Dictionary<string, List<int>>());
                        }
                        else
                        {
                            if (!studentsByCourse[courseName].ContainsKey(username))
                            {
                                studentsByCourse[courseName].Add(username, new List<int>());
                            }

                            studentsByCourse[courseName][username].Add(studentScoreOnTask);
                        }
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
