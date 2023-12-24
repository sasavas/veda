using MediatR;

namespace Veda.Application.UseCases.CustomerUseCases;

public record AddRecipientToCustomer() : IRequest;

public class AddRecipientToCustomerCommandHandler : IRequestHandler<AddRecipientToCustomer>
{
    public Task Handle(AddRecipientToCustomer request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}