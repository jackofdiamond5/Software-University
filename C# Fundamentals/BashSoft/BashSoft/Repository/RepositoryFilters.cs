using System;
using System.Collections.Generic;
using System.Linq;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class RepositoryFilter
    {
        public void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            switch (wantedFilter)
            {
                case "excellent":
                    FilterAndTake(wantedData, x => x >= 5.5, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(wantedData, x => x < 4.5 && x >= 2.5, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(wantedData, x => x < 2.5, studentsToTake);
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidStudentsFilter);
                    break;
            }
        }

        private void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter,
            int studentsToTake)
        {
            var counterForPrinted = 0;
            foreach (var userNamePoints in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                var averageMark = userNamePoints.Value.Average();
                var percentageOfAll = averageMark / 100;
                var mark = percentageOfAll * 6;
                if (!givenFilter(mark))
                    continue;

                OutputWriter.PrintStudent(userNamePoints);
                counterForPrinted++;
            }
        }
    }
}
