using System;
using System.Linq;
using System.Reflection;

public class Program
{
    static void Main()
    {
        try
        {
            var l = double.Parse(Console.ReadLine());
            var w = double.Parse(Console.ReadLine());
            var h = double.Parse(Console.ReadLine());

            var box = new Box(l, w, h);
            Console.WriteLine($"Surface Area - {box.GetSurfaceArea():F2}");
            Console.WriteLine($"Lateral Surface Area - {box.GetLateralSurfaceArea():F2}");
            Console.WriteLine($"Volume - {box.GetVolume():F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}