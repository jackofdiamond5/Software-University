using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        var input = Console.ReadLine();

        int[] galaxyDimensions = ParseInput(input);
        int galaxyX = galaxyDimensions[0];
        int galaxyY = galaxyDimensions[1];

        int[,] galaxyMatrix = new int[galaxyX, galaxyY];
        PopulateMatrix(galaxyMatrix, galaxyX, galaxyY);

        string command;
        long starsGathered = 0;
        while ((command = Console.ReadLine()) != "Let the Force be with you")
        {
            int[] ivoCoordinates = ParseInput(command);
            int[] evilForceCoordinates = ParseInput(Console.ReadLine());

            int xEvil = evilForceCoordinates[0];
            int yEvil = evilForceCoordinates[1];
            while (xEvil >= 0 && yEvil >= 0)
            {
                if (xEvil >= 0 && xEvil < galaxyMatrix.GetLength(0) && yEvil >= 0 && yEvil < galaxyMatrix.GetLength(1))
                {
                    galaxyMatrix[xEvil, yEvil] = 0;
                }

                xEvil--;
                yEvil--;
            }

            int xIvo = ivoCoordinates[0];
            int yIvo = ivoCoordinates[1];
            while (xIvo >= 0 && yIvo < galaxyMatrix.GetLength(1))
            {
                if (xIvo >= 0 && xIvo < galaxyMatrix.GetLength(0) && yIvo >= 0 && yIvo < galaxyMatrix.GetLength(1))
                {
                    starsGathered += galaxyMatrix[xIvo, yIvo];
                }

                yIvo++;
                xIvo--;
            }

            Console.WriteLine(starsGathered);
        }
    }

    public static void PopulateMatrix(int[,] galaxyMatrix, int galaxyX, int galaxyY)
    {
        int coordinateValue = 0;
        for (int r = 0; r < galaxyX; r++)
        {
            for (int c = 0; c < galaxyY; c++)
            {
                galaxyMatrix[r, c] = coordinateValue++;
            }
        }
    }

    public static int[] ParseInput(string command)
    {
        return command.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}