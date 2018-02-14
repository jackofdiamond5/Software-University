using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var args = Console.ReadLine().Split();
        var rectanglesCount = int.Parse(args[0]);
        var intersectionChecks = int.Parse(args[1]);

        var rectangles = new List<Rectangle>();
        for (var i = 0; i < rectanglesCount; i++)
        {
            var data = Console.ReadLine().Split();
            var id = data[0];
            var width = double.Parse(data[1]);
            var height = double.Parse(data[2]);
            var topLeftX = double.Parse(data[3]);
            var topLeftY = double.Parse(data[4]);

            var rectangle = new Rectangle(id, width, height, topLeftX, topLeftY);
            rectangles.Add(rectangle);
        }

        for (var i = 0; i < intersectionChecks; i++)
        {
            var data = Console.ReadLine().Split();
            var firstRectangleId = data[0];
            var secondRectangleId = data[1];

            var rectanglesToCheck = rectangles
                .Where(r => r.Id == firstRectangleId || r.Id == secondRectangleId)
                .ToArray();

            Console.WriteLine(Rectangle.RectanglesIntersect(rectanglesToCheck[0], rectanglesToCheck[1]) ? "true" : "false");
        }
    }
}

