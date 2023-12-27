namespace Veda.Application.SharedKernel.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string entityName) : base(entityName + " not found")
    {
    }

    public NotFoundException(string entityName, Exception? innerException) 
        : base(entityName + " not found", innerException)
    {
    }
}