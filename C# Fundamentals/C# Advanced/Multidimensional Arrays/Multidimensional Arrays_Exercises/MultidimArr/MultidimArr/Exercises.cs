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
        /// Exercise 3
        /// </summary>
        public static void MaximumSum()
        {
            var dimensions = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var matrix = new int[dimensions[0], dimensions[1]];

            for (var row = 0; row < dimensions[0]; row++)
            {
                var data = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var dimensionLength = matrix.GetLength(1);

                for (var column = 0; column < dimensionLength; column++)
                {
                    matrix[row, column] = data.Skip(column).Take(1).Min();
                }
            }

            var maxSum = long.MinValue;
            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0, i = 0;
            for (var row = 0; row < matrix.GetLength(0) - 2; row++)
            {
                for (var column = 0; column < matrix.GetLength(1) - 2; column++)
                {
                    var currentSum = matrix[row, column] + matrix[row, column + 1] + matrix[row, column + 2] +
                                     matrix[row + 1, column] + matrix[row + 1, column + 1] +
                                     matrix[row + 1, column + 2] +
                                     matrix[row + 2, column] + matrix[row + 2, column + 1] +
                                     matrix[row + 2, column + 2];

                    if (maxSum >= currentSum)
                        continue;

                    maxSum = currentSum;

                    a = matrix[row, column];
                    b = matrix[row, column + 1];
                    c = matrix[row, column + 2];
                    d = matrix[row + 1, column];
                    e = matrix[row + 1, column + 1];
                    f = matrix[row + 1, column + 2];
                    g = matrix[row + 2, column];
                    h = matrix[row + 2, column + 1];
                    i = matrix[row + 2, column + 2];
                }
            }

            Console.WriteLine($"Sum = {maxSum}");
            Console.WriteLine($"{a} {b} {c}");
            Console.WriteLine($"{d} {e} {f}");
            Console.WriteLine($"{g} {h} {i}");
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void DiagonalDifference()
        {
            var size = int.Parse(Console.ReadLine());
            var matrix = new int[size][];

            for (var i = 0; i < size; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            var firstDiagonal = 0L;
            var secondDiagonal = 0L;

            for (var i = 0; i < matrix.Length; i++)
            {
                firstDiagonal += matrix[i][i];
                secondDiagonal += matrix[i][matrix.Length - 1 - i];
            }

            var result = Math.Abs(firstDiagonal - secondDiagonal);

            Console.WriteLine(result);
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        public static void MatrixOfPalindromes()
        {
            var size = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var mRows = size[0];
            var mColumns = size[1];
            var matrix = new string[mRows][];

            for (var row = 0; row < mRows; row++)
            {
                matrix[row] = new string[mColumns];

                for (var column = 0; column < mColumns; column++)
                {
                    var first = (char)(97 + row);
                    var second = (char)(first + column);

                    var palindrome = new StringBuilder();
                    palindrome
                        .Append(first)
                        .Append(second)
                        .Append(first);

                    matrix[row][column] = palindrome.ToString();
                }
            }

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
    }
}
