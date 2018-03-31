using System.Collections.Generic;

public class NameComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        return x.CompareTo(y);
    }
}
