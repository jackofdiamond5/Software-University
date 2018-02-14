using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var listOfPeople = new List<Person>();
        for (int i = 0; i < n; i++)
        {
            var line = Console.ReadLine().Split();

            listOfPeople.Add(new Person(
                line[0], int.Parse(line[1]
                )
            ));
        }

        var filteredPeople = listOfPeople.Where(p => p.Age > 30).ToList();

        var sortedPeople = new SortedDictionary<string, int>();

        foreach (var person in filteredPeople)
        {
            sortedPeople.Add(person.Name, person.Age);
        }

        foreach (var person in sortedPeople)
        {
            Console.WriteLine($"{person.Key} - {person.Value}");
        }
    }
}