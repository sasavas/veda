using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record GetCustomerFilesRequest(int CustomerId) : IRequest<IEnumerable<Folder>>;

public class GetCustomerFilesRequestHandler(
    IRepository<Customer> customerRepository,
    IRepository<Recipient> recipientRepository)
    : IRequestHandler<GetCustomerFilesRequest, IEnumerable<Folder>>
{
    public Task<IEnumerable<Folder>> Handle(GetCustomerFilesRequest request, CancellationToken cancellationToken)
    {
        var customer = customerRepository.GetById(request.CustomerId) ?? throw new NotFoundException(nameof(Customer));
        var folders = customer.RecipientIds
            .Select(recipientRepository.GetById)
            .Where(recipient => recipient is not null)
            .SelectMany(recipient => recipient!.Folders);

        return Task.FromResult(folders);
    }
}