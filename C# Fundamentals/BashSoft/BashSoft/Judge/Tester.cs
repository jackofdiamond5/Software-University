using System;
using System.IO;
using System.Collections.Generic;

using BashSoft.IO;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.StaticData;

namespace BashSoft.Judge
{
    public class Tester : IContentComparer
    {
        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            try
            {
                OutputWriter.WriteMessageOnNewLine("Reading files...");

                var mismatchPath = GetMismatchPath(expectedOutputPath);

                var actualOutputLines = File.ReadAllLines(userOutputPath);
                var expectedOutputLines = File.ReadAllLines(expectedOutputPath);

                bool hasMismatch;
                var mismatches = GetAllPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

                PrintOutput(mismatches, hasMismatch, mismatchPath);
                OutputWriter.WriteMessageOnNewLine("Files read!");
            }
            catch (IOException)
            {
                throw new InvalidPathException();
            }
        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (!hasMismatch)
                return;

            foreach (var line in mismatches)
            {
                OutputWriter.WriteMessageOnNewLine(line);
            }

            File.WriteAllLines(mismatchPath, mismatches);

            OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
        }

        private string[] GetAllPossibleMismatches(IReadOnlyList<string> actualOutputLines, IReadOnlyList<string> expectedOutputLines, out bool hasMismatch)
        {
            hasMismatch = false;

            OutputWriter.WriteMessageOnNewLine("Comparing files...");

            var minOutputLines = actualOutputLines.Count;
            if (actualOutputLines.Count != expectedOutputLines.Count)
            {
                hasMismatch = true;
                minOutputLines = Math.Min(actualOutputLines.Count, expectedOutputLines.Count);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            var mismatches = new string[minOutputLines];
            for (var i = 0; i < minOutputLines; i++)
            {
                var actualLine = actualOutputLines[i];
                var expectedLine = expectedOutputLines[i];

                string output;
                if (!actualLine.Equals(expectedLine))
                {
                    output = $"Mismatch at line {i} -- expected: {expectedLine}, actual: {actualLine}";
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }

                mismatches[i] = output;
            }

            return mismatches;
        }

        private string GetMismatchPath(string expectedOutputPath)
        {
            var indexOf = expectedOutputPath.LastIndexOf("\\", StringComparison.InvariantCulture);
            var directoryPath = expectedOutputPath.Substring(0, indexOf);
            var finalPath = string.Concat(directoryPath, @"\Mismatches.txt");

            return finalPath;
        }
    }
}
