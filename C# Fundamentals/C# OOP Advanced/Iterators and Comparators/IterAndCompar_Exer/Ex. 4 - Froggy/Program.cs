using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var input = Console.ReadLine()
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var lake = new Lake<int>(input);

        var jumps = new Queue<int>();

        var index = 0;
        foreach (var stoneNum in lake)
        {
            if (index % 2 == 0)
            {
                jumps.Enqueue(stoneNum);
            }
            index++;
        }

        index = input.Count % 2 == 0 ? 0 : 1;
        foreach (var stoneNum in lake.Reverse())
        {
            if (index % 2 == 0)
            {
                jumps.Enqueue(stoneNum);
            }
            index++;
        }

        Console.WriteLine(string.Join(", ", jumps));
    }
}
