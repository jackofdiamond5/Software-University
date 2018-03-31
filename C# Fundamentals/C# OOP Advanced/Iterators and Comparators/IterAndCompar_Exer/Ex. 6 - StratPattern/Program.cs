using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var setForNames = new SortedSet<Person>(new NameComparer());
        var setForAge = new SortedSet<Person>(new AgeComparer());

        var n = int.Parse(Console.ReadLine());
        for(var i = 0; i < n; i++)
        {
            var args = Console.ReadLine().Split();
            setForNames.Add(new Person(args[0], int.Parse(args[1])));
            setForAge.Add(new Person(args[0], int.Parse(args[1])));
        }

        foreach(var person in setForNames)
        {
            Console.WriteLine($"{person.Name} {person.Age}");
        }

        foreach (var person in setForAge)
        {
            Console.WriteLine($"{person.Name} {person.Age}");
        }
    }
}
