namespace Store.Exceptions;

public class ClientAlreadyExistsException : Exception
{
    public ClientAlreadyExistsException() : base()
    {
    }

    public ClientAlreadyExistsException(string? message) : base(message)
    {
    }

    public ClientAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}