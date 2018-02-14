using System;

public class Program
{
    public static void Main()
    {
        var firstDate = DateTime.Parse(Console.ReadLine());
        var secondDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine(DateModifier.CalcuateDifferenceBetweenDates(firstDate, secondDate));
    }
}

