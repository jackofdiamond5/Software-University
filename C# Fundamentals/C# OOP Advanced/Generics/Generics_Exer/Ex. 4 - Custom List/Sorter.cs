using System;
using System.Collections.Generic;

public static class Sorter
{
    public static CustomList<T> Sort<T>(CustomList<T> list)
        where T : IComparable
    {
        return list.Sort();
    }
}
