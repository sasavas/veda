namespace Veda.Application.Exceptions;

public class VedaApplicationException : Exception
{
    public VedaApplicationException(string? message) : base(message)
    {
    }

    public VedaApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}