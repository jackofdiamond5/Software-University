using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IDataFilterer
    {
        void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake);
    }
}
