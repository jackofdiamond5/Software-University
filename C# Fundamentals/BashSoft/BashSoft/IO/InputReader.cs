using System;

using BashSoft.Contracts;
using BashSoft.StaticData;

namespace BashSoft.IO
{
    public class InputReader : IReader
    {
        private const string EndCommand = "quit";
        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
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
