using System;

public class Program
{
    static void Main()
    {
        var firstLine = Console.ReadLine().Split();
        var secondLine = Console.ReadLine().Split();
        var thirdLine = Console.ReadLine().Split();

        var firstPerson = 
            new ThreeUple<string, string, string>(string.Join(" ", firstLine[0], firstLine[1]), firstLine[2], firstLine[3]);
        var drunk = secondLine[2] == "drunk" ? true : false;
        var secondPerson = 
            new ThreeUple<string, int, bool>(secondLine[0], int.Parse(secondLine[1]), drunk);
        var thirdPerson = 
            new ThreeUple<string, double, string>(thirdLine[0], double.Parse(thirdLine[1]), thirdLine[2]);

        Console.WriteLine(firstPerson);
        Console.WriteLine(secondPerson);
        Console.WriteLine(thirdPerson);
    }
}
