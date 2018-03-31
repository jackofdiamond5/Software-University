using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var hashCol = new HashSet<Person>();
        var sortedCol = new SortedSet<Person>();

        var n = int.Parse(Console.ReadLine());
        for(var i = 0; i < n; i++)
        {
            var args = Console.ReadLine().Split();
            var person = new Person(args[0], int.Parse(args[1]));

            hashCol.Add(person);

            sortedCol.Add(person);
        }

        Console.WriteLine(sortedCol.Count);
        Console.WriteLine(hashCol.Count);
    }
}
