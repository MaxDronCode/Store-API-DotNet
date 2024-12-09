namespace Store.Api.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(string message) : base(message)
    {
    }

    public ClientNotFoundException() : base()
    {
    }

    public ClientNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}