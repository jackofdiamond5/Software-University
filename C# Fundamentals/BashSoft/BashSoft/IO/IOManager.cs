using System;
using System.Collections.Generic;
using System.IO;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public static class IoManager
    {
        public static void CreateDirectoryInCurrentFolder(string folderName)
        {
            var path = SessionData.CurrentPath + "\\" + folderName;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                OutputWriter.DisplayException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        public static void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            var initIndentation = SessionData.CurrentPath.Split('\\').Length;
            var subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.CurrentPath);

            while (subFolders.Count != 0)
            {
                var currentPath = subFolders.Dequeue();

                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        var indexOfLastSlah = file.LastIndexOf("\\", StringComparison.InvariantCulture);
                        var fileName = file.Substring(indexOfLastSlah);
                        OutputWriter.WriteMessageOnNewLine($"{new string('-', indexOfLastSlah)}{fileName}");
                    }

                    foreach (var dirPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(dirPath);
                    }
                }
                catch (Exception)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }

                var indentation = currentPath.Split('\\').Length - initIndentation;
                if (depth - indentation < 0)
                {
                    break;
                }

                OutputWriter.WriteMessageOnNewLine($"{indentation} - {currentPath}");
            }
        }

        public static void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    var currentPath = SessionData.CurrentPath;
                    var indexOfLastSlah = currentPath.LastIndexOf("\\", StringComparison.InvariantCulture);
                    var newPath = currentPath.Substring(0, indexOfLastSlah);
                    SessionData.CurrentPath = newPath;

                }
                catch (Exception)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToGetHigherPartitionHierarchy);
                }
            }
            else
            {
                var currentPath = SessionData.CurrentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public static void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                return;
            }

            SessionData.CurrentPath = absolutePath;
        }
    }
}
