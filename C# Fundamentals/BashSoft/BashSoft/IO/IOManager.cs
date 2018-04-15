using System;
using System.IO;
using System.Collections.Generic;

using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.StaticData;

namespace BashSoft.IO
{
    public class IoManager : IDirectoryManager
    {
        public void CreateDirectoryInCurrentFolder(string folderName)
        {
            var path = SessionData.CurrentPath + "\\" + folderName;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        public void TraverseDirectory(int depth)
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

        public void ChangeCurrentDirectoryRelative(string relativePath)
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
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("indexOfLastSlash", ExceptionMessages.InvalidDestination);
                }
            }
            else
            {
                var currentPath = SessionData.CurrentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }

            SessionData.CurrentPath = absolutePath;
        }
    }
}
