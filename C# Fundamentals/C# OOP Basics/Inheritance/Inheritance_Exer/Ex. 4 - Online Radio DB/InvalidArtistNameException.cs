using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidArtistNameException : SystemException
{
    public InvalidArtistNameException()
    {
    }

    public InvalidArtistNameException(string message) : base(message)
    {
    }

    public InvalidArtistNameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidArtistNameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}