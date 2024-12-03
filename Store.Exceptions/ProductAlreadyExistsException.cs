namespace Store.Exceptions;

public class ProductAlreadyExistsException : Exception
{
    public ProductAlreadyExistsException() : base("Product with this name already exists")
    {
    }

    public ProductAlreadyExistsException(string? message) : base(message)
    {
    }

    public ProductAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}