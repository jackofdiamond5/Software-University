using System;

public class Program
{
    static void Main()
    {
        var l = double.Parse(Console.ReadLine());
        var w = double.Parse(Console.ReadLine());
        var h = double.Parse(Console.ReadLine());

        var box = new Box(l, w, h);
        Console.WriteLine($"Surface Area - {box.GetSurfaceArea():F2}");
        Console.WriteLine($"Lateral Surface Area - {box.GetLateralSurfaceArea():F2}");
        Console.WriteLine($"Volume - {box.GetVolume():F2}");
    }
}