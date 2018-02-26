using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidSongException : Exception
{
    public InvalidSongException()
    {
    }

    public InvalidSongException(string message) : base(message)
    {
    }

    public InvalidSongException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidSongException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}