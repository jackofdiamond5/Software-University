using System;
using System.IO;
using System.Linq;
using System.Reflection;

using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.Attributes;
using BashSoft.IO.Commands;

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
            var parametersForConstructors = new object[] { input, data };

            var typeOfCommand = Assembly.GetExecutingAssembly()
                .GetTypes()
                .First(type => type.GetCustomAttributes(typeof(AliasAttribute))
                    .Where(atr => atr.Equals(command))
                    .ToArray().Length > 0);

            var typeOfInterpreter = typeof(CommandInterpreter);

            var exe = (Command)Activator.CreateInstance(typeOfCommand, parametersForConstructors);

            var fieldsOfCommand = typeOfCommand
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            var fieldsOfIntepreter = typeOfInterpreter
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                var atrAttribute = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));
                if (atrAttribute != null)
                {
                    if (fieldsOfIntepreter.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe,
                            fieldsOfIntepreter.First(x => x.FieldType == fieldOfCommand.FieldType)
                            .GetValue(this));
                    }
                }
            }

            return exe;
        }
    }
}
