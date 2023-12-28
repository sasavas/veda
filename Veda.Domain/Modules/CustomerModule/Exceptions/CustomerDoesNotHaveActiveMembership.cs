using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.Modules.CustomerModule.Exceptions;

public class CustomerDoesNotHaveActiveMembership : DomainException
{
    public CustomerDoesNotHaveActiveMembership(string? message) : base(message)
    {
    }

    public CustomerDoesNotHaveActiveMembership(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}