using MediatR;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record GetCustomerFilesRequest(int CustomerId) : IRequest<IEnumerable<DigitalContent>>;

public class GetCustomerFilesRequestHandler(
    ICustomerRepository customerRepository)
    : IRequestHandler<GetCustomerFilesRequest, IEnumerable<DigitalContent>>
{
    public Task<IEnumerable<DigitalContent>> Handle(GetCustomerFilesRequest request, CancellationToken cancellationToken)
    {
        var customer = customerRepository.GetByIdIncludingRecipientsAndContens(request.CustomerId) ?? throw new NotFoundException(nameof(Customer));
        var contents = customer
            .Recipients
            .Select(recipient => recipient.Folder)
            .SelectMany(folder => folder.DigitalContents);

        return Task.FromResult(contents);
    }
}