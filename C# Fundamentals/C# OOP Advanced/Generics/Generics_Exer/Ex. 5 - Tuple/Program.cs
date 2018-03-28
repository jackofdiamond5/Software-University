using System;

public class Program
{
    static void Main()
    {
        var firstLine = Console.ReadLine().Split();
        var secondLine = Console.ReadLine().Split();
        var thirdLine = Console.ReadLine().Split();

        var firstTuple = 
            new CustomTuple<string, string>(string.Join(" ", firstLine[0], firstLine[1]), firstLine[2]);
        var secondTuple = new CustomTuple<string, int>(secondLine[0], int.Parse(secondLine[1]));
        var thirdTuple = new CustomTuple<int, double>(int.Parse(thirdLine[0]), double.Parse(thirdLine[1]));

        Console.WriteLine(firstTuple);
        Console.WriteLine(secondTuple);
        Console.WriteLine(thirdTuple);
    }
}
