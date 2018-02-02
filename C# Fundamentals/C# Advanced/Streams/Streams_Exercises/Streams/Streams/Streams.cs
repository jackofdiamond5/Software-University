using System;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Collections.Generic;

namespace Streams
{
    public class Streams
    {
        private const string DirectoryPath = "../../../resources";

        public static void Main()
        {
            //OddLines();
            //LineNumbers();
            //WordCount();
            //CopyBinaryFile();

            //Slice($"{DirectoryPath}/sliceMe.mp4", 5);
            //var files = new List<string>()
            //{
            //    "Part - 0.mp4",
            //    "Part - 1.mp4",
            //    "Part - 2.mp4",
            //    "Part - 3.mp4",
            //    "Part - 4.mp4"
            //};
            //Assemble(files);

            //ZipSlicedFles($"{DirectoryPath}/sliceMe.mp4", 5);
            //var zippedFiles = new List<string>()
            //{
            //    "Part - 0.mp4.gz",
            //    "Part - 1.mp4.gz",
            //    "Part - 2.mp4.gz",
            //    "Part - 3.mp4.gz",
            //    "Part - 4.mp4.gz"
            //};
            //UnzipAndAssemble(zippedFiles);

            //DirectoryTraversal();
        }

        /// <summary>
        /// Exercise 6
        /// </summary>
        public static void DirectoryTraversal()
        {
            var path = Console.ReadLine();
            var fileDict = new Dictionary<string, List<FileInfo>>();

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var extension = fileInfo.Extension;

                if (!fileDict.ContainsKey(extension))
                {
                    fileDict[extension] = new List<FileInfo>();
                }

                fileDict[extension].Add(fileInfo);
            }

            fileDict = fileDict.OrderByDescending(f => f.Value.Count)
                .ThenBy(f => f.Key)
                .ToDictionary(k => k.Key, v => v.Value);

            var fileName = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/report.txt";

            using (var writer = new StreamWriter(fileName))
            {
                foreach (var kvp in fileDict)
                {
                    var extension = kvp.Key;
                    writer.WriteLine(extension);

                    var fileInfoList = kvp.Value;

                    foreach (var fi in fileInfoList.OrderByDescending(fi => fi.Length))
                    {
                        var fileSize = (double) fi.Length / 1024;

                        writer.WriteLine($"--{fi.Name} - {fileSize:F3}kb");
                    }
                }
            }
        }

        /// <summary>
        /// Exercise 5
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="partsCount"></param>
        public static void ZipSlicedFles(string filePath, int partsCount)
        {
            using (var reader = new FileStream(filePath, FileMode.Open))
            {
                var pieceSize = (long)Math.Ceiling((double)reader.Length / partsCount);
                var extension = filePath.Split('.').Reverse().ToArray()[0];

                for (var i = 0; i < partsCount; i++)
                {
                    var part = $"{DirectoryPath}/Part - {i}.{extension}.gz";
                    var sizeSum = 0L;

                    using (var writer = new GZipStream(new FileStream(part, FileMode.Create), CompressionLevel.Optimal))
                    {
                        var buffer = new byte[4096];
                        while (reader.Read(buffer, 0, buffer.Length) == buffer.Length)
                        {
                            writer.Write(buffer, 0, buffer.Length);
                            sizeSum += buffer.Length;

                            if (sizeSum > pieceSize)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static void UnzipAndAssemble(List<string> zippedFiles)
        {
            var extension = zippedFiles[0].Split('.').Reverse().Skip(1).ToArray()[0];
            var assembled = $"{DirectoryPath}/DecompressedAndAssembledFile.{extension}";
            var buffer = new byte[4096];

            using (var writer = new FileStream(assembled, FileMode.Create))
            {
                foreach (var file in zippedFiles)
                {
                    using (var reader = new GZipStream(new FileStream($"{DirectoryPath}/{file}", FileMode.Open), CompressionMode.Decompress))
                    {
                        while (reader.Read(buffer, 0, buffer.Length) == buffer.Length)
                        {
                            writer.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Exercise 5
        /// </summary>
        public static void Slice(string filePath, int partsCount)
        {
            using (var reader = new FileStream(filePath, FileMode.Open))
            {
                var pieceSize = (long)Math.Ceiling((double)reader.Length / partsCount);
                var extension = filePath.Split('.').Reverse().ToArray()[0];

                for (var i = 0; i < partsCount; i++)
                {
                    var part = $"{DirectoryPath}/Part - {i}.{extension}";
                    var sizeSum = 0L;

                    using (var writer = new FileStream(part, FileMode.Create))
                    {
                        var buffer = new byte[4096];
                        while (reader.Read(buffer, 0, buffer.Length) == buffer.Length)
                        {
                            writer.Write(buffer, 0, buffer.Length);
                            sizeSum += buffer.Length;

                            if (sizeSum > pieceSize)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static void Assemble(List<string> files)
        {
            var extension = files[0].Split('.').Reverse().ToArray()[0];
            var assembled = $"{DirectoryPath}/AssembledFile.{extension}";
            var buffer = new byte[4096];

            using (var writer = new FileStream(assembled, FileMode.Create))
            {
                foreach (var file in files)
                {
                    using (var reader = new FileStream($"{DirectoryPath}/{file}", FileMode.Open))
                    {
                        while (reader.Read(buffer, 0, buffer.Length) == buffer.Length)
                        {
                            writer.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Exercise 4
        /// </summary>
        public static void CopyBinaryFile()
        {
            using (var source = new FileStream($"{DirectoryPath}/copyMe.png", FileMode.Open))
            {
                using (var destination = new FileStream($"{DirectoryPath}/copied.png", FileMode.Create))
                {
                    var buffer = new byte[4096];
                    while (true)
                    {
                        var bytes = source.Read(buffer, 0, buffer.Length);
                        if (bytes <= 0)
                        {
                            break;
                        }

                        destination.Write(buffer, 0, bytes);
                    }
                }
            }
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        public static void WordCount()
        {
            var words = File.ReadAllLines($"{DirectoryPath}/words.txt");
            var wordsDict = new Dictionary<string, int>();
            foreach (var key in words)
            {
                wordsDict[key] = 0;
            }

            using (var reader = new StreamReader($"{DirectoryPath}/text.txt"))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    var lineParts = currentLine
                        .Split(new[] { ' ', ',', '-', '!', '?', '.' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var part in lineParts)
                    {
                        if (wordsDict.ContainsKey(part.ToLower()))
                        {
                            wordsDict[part.ToLower()]++;
                        }
                    }
                }
            }

            using (var writer = new StreamWriter($"{DirectoryPath}/result.txt"))
            {
                foreach (var kvp in wordsDict)
                {
                    writer.WriteLine($"{kvp.Key} - {kvp.Value}");
                }
            }
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void LineNumbers()
        {
            using (var reader = new StreamReader($"{DirectoryPath}/text.txt"))
            {
                var counter = 1;
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Line {counter}: {currentLine}");

                    counter++;
                }
            }
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        public static void OddLines()
        {
            using (var reader = new StreamReader($"{DirectoryPath}/text.txt"))
            {
                var counter = 0;
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if (counter % 2 != 0)
                    {
                        Console.WriteLine(currentLine);
                    }

                    counter++;
                }
            }
        }
    }
}
