using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.CustomerUseCases;

public record GetRecipientsRequest(int CustomerId) : IRequest<IEnumerable<Recipient>>;

public class GetRecipientsRequestHandler(
    IRepository<Customer> customerRepository)
    : IRequestHandler<GetRecipientsRequest, IEnumerable<Recipient>>
{
    public Task<IEnumerable<Recipient>> Handle(GetRecipientsRequest request, CancellationToken cancellationToken)
    {
        var customer = customerRepository.GetById(request.CustomerId) ?? throw new NotFoundException(nameof(Customer));

        return Task.FromResult(customer.Recipients.AsEnumerable());
    }
}