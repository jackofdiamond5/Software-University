using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        GenericSwapStrings();
        GenericSwapIntegers();
    }

    public static void GenericSwapIntegers()
    {
        var n = int.Parse(Console.ReadLine());
        var boxesList = new List<Box<int>>();

        for (var i = 0; i < n; i++)
        {
            var input = int.Parse(Console.ReadLine());
            var box = new Box<int>(input);
            boxesList.Add(box);
        }

        var indexes = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        GenericSwap(boxesList, indexes[0], indexes[1]);

        boxesList.ForEach(Console.WriteLine);
    }

    /// <summary>
    /// Task 1
    /// </summary>
    public static void GenericSwapStrings()
    {
        var n = int.Parse(Console.ReadLine());
        var boxesList = new List<Box<string>>();

        for (var i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            var box = new Box<string>(input);
            boxesList.Add(box);
        }

        var indexes = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        GenericSwap(boxesList, indexes[0], indexes[1]);

        boxesList.ForEach(Console.WriteLine);
    }

    public static void GenericSwap<T>(List<T> list, int firstIndex, int secondIndex)
    {
        var temp = list[firstIndex];
        list[firstIndex] = list[secondIndex];
        list[secondIndex] = temp;
    }
}
