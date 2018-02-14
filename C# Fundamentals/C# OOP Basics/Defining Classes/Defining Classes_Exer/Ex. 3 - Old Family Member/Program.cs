using System;

public class Program
{
    public static void Main()
    {
        var peopleCount = int.Parse(Console.ReadLine());
        var family = new Family();

        for (var i = 0; i < peopleCount; i++)
        {
            var args = Console.ReadLine().Split();
            var person = new Person(int.Parse(args[1]), args[0]);

            family.AddMembers(person);
        }

        Console.WriteLine(family.GetOldestMember().ToString());
    }
}