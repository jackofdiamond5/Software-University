using System.Linq;
using System.Collections.Generic;

using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class RepositorySorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            switch (comparison)
            {
                case "ascending":
                    PrintStudents(studentsWithMarks
                        .OrderBy(s => s.Value)
                        .Take(studentsToTake)
                        .ToDictionary(p => p.Key, p => p.Value));
                    break;
                case "descending":
                    PrintStudents(studentsWithMarks
                        .OrderByDescending(s => s.Value)
                        .Take(studentsToTake)
                        .ToDictionary(p => p.Key, p => p.Value));
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
                    break;
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (var kvp in studentsSorted)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}
