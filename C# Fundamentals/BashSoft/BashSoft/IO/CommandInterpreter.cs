using System;
using System.IO;
using System.Diagnostics;

using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public class CommandInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IoManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IoManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            var data = input.Split();
            var command = data[0];

            try
            {
                ParseCommand(input, command, data);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayException(dnfe.Message);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                OutputWriter.DisplayException(aoore.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch(Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private void ParseCommand(string input, string command, string[] data)
        {
            switch (command.ToLower())
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdrel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdabs":
                    TryCHangePathAbsolute(input, data);
                    break;
                case "readdb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                case "decorder":
                    break;
                case "download":
                    break;
                case "downloadasynch":
                    break;
                case "trydropdb":
                    TryDropDb(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void TryDropDb(string input, string[] data)
        {
            if(data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
                return;
            }

            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }

        private void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var filter = data[2].ToLower();
                var orderCommand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                TryParseParemetersForOrderAndTake(orderCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParemetersForOrderAndTake(string orderCommand, string takeQuantity, string courseName, string filter)
        {
            if (orderCommand == "order")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, filter);
                }
                else
                {
                    var wasParsed = int.TryParse(takeQuantity, out int studentsToTake);

                    if (wasParsed)
                    {
                        this.repository.OrderAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidOrderCommand);
            }
        }

        private void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                var courseName = data[1];
                var filter = data[2].ToLower();
                var takeCommand = data[3].ToLower();
                var takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryParseParametersForFilterAndTake(
            string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    var wasParsed = int.TryParse(takeQuantity, out int studentsToTake);

                    if (wasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }

        private void TryShowWantedData(string input, string[] data)
        {
            switch (data.Length)
            {
                case 2:
                    {
                        var courseName = data[1];
                        this.repository.GetAllStudentsFromCourse(courseName);
                    }
                    break;
                case 3:
                    {
                        var courseName = data[1];
                        var userName = data[2];
                        this.repository.GetStudentScoresFromCourse(courseName, userName);
                    }
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private static void TryGetHelp(string input, string[] data)
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteMessageOnNewLine($"|{"make directory - mkdir: path ",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"traverse directory - ls: depth ",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"comparing files - cmp: path1 path2",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"change directory - changeDirREl:relative path",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"change directory - changeDir:absolute path",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"read students data base - readDb: path",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"download file - download: path of file (saved in current directory)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"download file asinchronously - downloadAsynch: path of file (save in the current directory)",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"get help – help",-98}|");
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteEmptyLine();
        }

        private void TryReadDatabaseFromFile(string input, string[] data)
        {
            var fileName = data[1];
            this.repository.LoadData(fileName);
        }

        private void TryCHangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var absolutePath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var relPath = data[1];
                this.inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                var firstPath = data[1];
                var secondPath = data[2];

                this.judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryTraverseFolders(string input, string[] data)
        {
            switch (data.Length)
            {
                case 1:
                    this.inputOutputManager.TraverseDirectory(0);
                    break;
                case 2:
                    var canBeParsed = int.TryParse(data[1], out int depth);

                    if (canBeParsed)
                    {
                        this.inputOutputManager.TraverseDirectory(depth);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                    }
                    break;
                default:
                    return;
            }
        }

        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var folderName = data[1];
                this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                var fileName = data[1];
                Process.Start(SessionData.CurrentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.WriteMessageOnNewLine($"The command {input} is invalid!");
        }
    }
}
