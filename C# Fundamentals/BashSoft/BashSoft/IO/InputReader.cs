using System;
using BashSoft.Static_data;

namespace BashSoft.IO
{
    public static class InputReader
    {
        private const string EndCommand = "quit";

        public static void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
            var input = Console.ReadLine();
            while (input != null && input.ToLower() != EndCommand)
            {
                var inputCommand = input.Trim().ToLower();
                CommandInterpreter.InterpretCommand(inputCommand);

                OutputWriter.WriteEmptyLine();
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
                input = Console.ReadLine();
            }
        }
    }
}
