using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var people = new List<Person>();
        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            var args = input.Split();
            var person = new Person(args[0], int.Parse(args[1]), args[2]);
            people.Add(person);
        }

        try
        {
            var nIndex = int.Parse(Console.ReadLine());
            var personN = people[nIndex];

            var equalPeople = people.Where(p => p.CompareTo(personN) == 0).Count();
            var notEqualPeople = people.Where(p => p.CompareTo(personN) == -1).Count();

            Console.WriteLine($"{equalPeople} {notEqualPeople} {people.Count}");
        }
        catch (Exception)
        {
            Console.WriteLine("No matches");
        }
    }
}
