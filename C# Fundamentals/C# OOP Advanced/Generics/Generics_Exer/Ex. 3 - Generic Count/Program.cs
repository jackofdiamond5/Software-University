using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        GenericCompareStrings();
        GenericCompareDoubles();
    }

    /// <summary>
    /// Task 2
    /// </summary>
    public static void GenericCompareDoubles()
    {
        var n = int.Parse(Console.ReadLine());
        var boxes = new List<Box<double>>();

        for (var i = 0; i < n; i++)
        {
            var input = double.Parse(Console.ReadLine());
            var box = new Box<double>(input);
            boxes.Add(box);
        }

        var elementToCompare = double.Parse(Console.ReadLine());
        var result = GenericCompare<Box<double>, double>(boxes, elementToCompare);

        Console.WriteLine(result);
    }

    /// <summary>
    /// Task 1
    /// </summary>
    public static void GenericCompareStrings()
    {
        var n = int.Parse(Console.ReadLine());
        var boxes = new List<Box<string>>();

        for (var i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            var box = new Box<string>(input);
            boxes.Add(box);
        }

        var elementToCompare = Console.ReadLine();
        var result = GenericCompare<Box<string>, string>(boxes, elementToCompare);

        Console.WriteLine(result);
    }
    
    public static int GenericCompare<T, T1>(IList<T> items, T1 elementToCompare)
        where T : IComparable
        where T1 : IComparable
    {
        return items.Count(e => e.CompareTo(elementToCompare) > 0);
    }
    
    public static int GenericCompare<T, T1>(ICollection<T> items, T1 elementToCompare)
        where T : IComparable<T1>
        where T1 : IComparable
    {
        return items.Count(e => e.CompareTo(elementToCompare) > 0);
    }
}
