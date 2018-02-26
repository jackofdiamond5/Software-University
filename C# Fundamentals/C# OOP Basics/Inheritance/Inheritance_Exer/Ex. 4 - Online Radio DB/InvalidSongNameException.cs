using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidSongNameException : Exception
{
    public InvalidSongNameException()
    {
    }

    public InvalidSongNameException(string message) : base(message)
    {
    }

    public InvalidSongNameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidSongNameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}