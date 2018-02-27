using System;

public class Program
{
    static void Main()
    {
        var driverName = Console.ReadLine();

        ICar ferrari = new Ferrari();

        Console.WriteLine($"488-Spider/{ferrari.Brakes}/{ferrari.Gas}/{driverName}");
    }
}
