using System;
using System.IO;

using BashSoft.Judge;
using BashSoft.Repository;
using BashSoft.IO.Commands;
using BashSoft.Exceptions;
using BashSoft.Contracts;

namespace BashSoft.IO
{
    public class CommandInterpreter : IInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            var data = input.Split();
            var commandName = data[0];
            
            try
            {
                var command = ParseCommand(input, commandName, data);
                command.Execute();
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
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            switch (command.ToLower())
            {
                case "open":
                    return new OpenFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "mkdir":
                    return new MakeDirectoryCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "ls":
                    return new TraverseFolderCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cmp":
                    return new CompareFilesCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdrel":
                    return new ChangePathRelativelyCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdabs":
                    return new ChangePathAbsoluteCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "readdb":
                    return new ReadDatabaseFromFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "help":
                    return new GetHelpCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "show":
                    return new ShowWantedDataCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "decorder":
                    throw new NotImplementedException();
                case "download":
                    throw new NotImplementedException();
                case "downloadasynch":
                    throw new NotImplementedException();
                case "trydropdb":
                    return new DropDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                default:
                    throw new InvalidCommandException(input);
            }
        }
    }
}
