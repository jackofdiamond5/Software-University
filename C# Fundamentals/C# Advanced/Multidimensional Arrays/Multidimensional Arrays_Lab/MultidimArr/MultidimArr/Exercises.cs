using System;
using System.Linq;
using System.Text;

namespace MultidimArr
{
    public class Exercises
    {
        public static void Main()
        {
        }

        /// <summary>
        /// Exercise 4
        /// </summary>
        public static void PascalTriangle()
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = new long[size][];

            for (var row = 0; row < size; row++)
            {
                matrix[row] = new long[row + 1];
                matrix[row][0] = 1;
                matrix[row][row] = 1;

                if (row < 2) continue;

                for (var col = 1; col < row; col++)
                {
                    matrix[row][col] = matrix[row - 1][col - 1] + matrix[row - 1][col];
                }
            }

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        public static void GroupNumbers()
        {
            var data = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            var resultSet = new StringBuilder[3];
            resultSet[0] = new StringBuilder();
            resultSet[1] = new StringBuilder();
            resultSet[2] = new StringBuilder();

            foreach (var number in data)
            {
                switch (Math.Abs(number % 3))
                {
                    case 0:
                        resultSet[0].Append($"{number} ");
                        break;
                    case 1:
                        resultSet[1].Append($"{number} ");
                        break;
                    case 2:
                        resultSet[2].Append($"{number} ");
                        break;
                }
            }

            Console.WriteLine(resultSet[0]);
            Console.WriteLine(resultSet[1]);
            Console.WriteLine(resultSet[2]);
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void SquareMaximumSum()
        {
            var dimensions = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var matrix = new int[dimensions[0], dimensions[1]];

            for (var row = 0; row < dimensions[0]; row++)
            {
                var data = Console.ReadLine()
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var dimensionLength = matrix.GetLength(1);

                for (var column = 0; column < dimensionLength; column++)
                {
                    matrix[row, column] = data.Skip(column).Take(1).Min();
                }
            }

            var maxSum = long.MinValue;
            int w = 0, x = 0, y = 0, z = 0;
            for (var row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (var column = 0; column < matrix.GetLength(1) - 1; column++)
                {
                    var currentSum = matrix[row, column] + matrix[row, column + 1] +
                                     matrix[row + 1, column] + matrix[row + 1, column + 1];

                    if (maxSum >= currentSum)
                        continue;

                    maxSum = currentSum;

                    w = matrix[row, column];
                    x = matrix[row, column + 1];
                    y = matrix[row + 1, column];
                    z = matrix[row + 1, column + 1];
                }
            }

            Console.WriteLine($"{w} {x}");
            Console.WriteLine($"{y} {z}");
            Console.WriteLine(maxSum);
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        public static void SumMatrixElements()
        {
            var dimensions = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var sum = 0;
            for (var rows = 0; rows < dimensions[0]; rows++)
            {
                var elements = Console.ReadLine()
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                sum += elements.Sum();
            }

            Console.WriteLine(dimensions[0]);
            Console.WriteLine(dimensions[1]);
            Console.WriteLine(sum);
        }
    }
}
