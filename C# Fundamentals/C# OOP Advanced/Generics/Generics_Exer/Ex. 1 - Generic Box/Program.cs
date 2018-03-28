using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        GenericBoxOfStrings();
        GenericBoxOfIntegers();
    }

    /// <summary>
    /// Task 2
    /// </summary>
    public static void GenericBoxOfIntegers()
    {
        var n = int.Parse(Console.ReadLine());

        var boxes = new Queue<Box<int>>();

        for (var i = 0; i < n; i++)
        {
            var input = int.Parse(Console.ReadLine());
            var box = new Box<int>(input);
            boxes.Enqueue(box);
        }

        foreach (var box in boxes)
        {
            Console.WriteLine(box);
        }
    }

    /// <summary>
    /// Task 1
    /// </summary>
    public static void GenericBoxOfStrings()
    {
        var n = int.Parse(Console.ReadLine());

        var boxes = new Queue<Box<string>>();

        for (var i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            var box = new Box<string>(input);
            boxes.Enqueue(box);
        }

        foreach (var box in boxes)
        {
            Console.WriteLine(box);
        }
    }
}
