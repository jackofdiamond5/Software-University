using System.Collections.Generic;
using System.Linq;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class RepositorySorter
    {
        public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            switch (comparison)
            {
                case "ascending":
                    PrintStudents(wantedData.OrderBy(
                        x => x.Value.Sum())
                        .Take(studentsToTake)
                        .ToDictionary(
                        p => p.Key, p => p.Value));
                    break;
                case "descending":
                    PrintStudents(wantedData.OrderByDescending(
                            x => x.Value.Sum())
                        .Take(studentsToTake)
                        .ToDictionary(
                            p => p.Key, p => p.Value));
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
                    break;
            }
        }

        private static void PrintStudents(Dictionary<string, List<int>> studentsSorted)
        {
            foreach (var kvp in studentsSorted)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}
