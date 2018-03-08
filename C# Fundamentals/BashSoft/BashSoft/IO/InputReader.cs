using System;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public class InputReader
    {
        private const string EndCommand = "quit";
        private CommandInterpreter interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
            var input = Console.ReadLine();
            while (input != null && input.ToLower() != EndCommand)
            {
                var inputCommand = input.Trim().ToLower();
                interpreter.InterpretCommand(inputCommand);

                OutputWriter.WriteEmptyLine();
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
                input = Console.ReadLine();
            }
        }
    }
}
