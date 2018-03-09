using System;

namespace BashSoft.Exceptions
{
    class InvalidCommandException : Exception
    {
        private const string InvalidCommandMessage = "The {0} command is invalid";

        public InvalidCommandException(string commandName)
        : base(string.Format(InvalidCommandMessage, commandName)) { }
    }
}
