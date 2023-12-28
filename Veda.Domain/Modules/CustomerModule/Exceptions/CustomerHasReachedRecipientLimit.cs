using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.Modules.CustomerModule.Exceptions;

public class CustomerHasReachedRecipientLimit : DomainException
{
    public CustomerHasReachedRecipientLimit(string? message) : base(message)
    {
        
    }

    public CustomerHasReachedRecipientLimit(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}