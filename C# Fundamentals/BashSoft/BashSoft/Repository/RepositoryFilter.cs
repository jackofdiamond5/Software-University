using System;
using System.Collections.Generic;

using BashSoft.IO;
using BashSoft.Contracts;
using BashSoft.StaticData;

namespace BashSoft.Repository
{
    public class RepositoryFilter : IDataFilterer
    {
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake)
        {
            switch (wantedFilter)
            {
                case "excellent":
                    FilterAndTake(studentsWithMarks, x => x >= 5.5, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(studentsWithMarks, x => x < 4.5 && x >= 2.5, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(studentsWithMarks, x => x < 2.5, studentsToTake);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidStudentsFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            var counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(studentMark);
                    counterForPrinted++;
                }
            }
        }
    }
}
