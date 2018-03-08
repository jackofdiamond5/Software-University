using System;

namespace BashSoft.Exceptions
{
    class DataAlreadyInitializedException : Exception
    {
        public const string DataAlreadyInitialized = "Data is already initialized!";

        public DataAlreadyInitializedException()
            : base(DataAlreadyInitialized) { }

        public DataAlreadyInitializedException(string message)
            : base(message) { }
    }
}
