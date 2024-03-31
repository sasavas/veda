using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports.DataAccess;

namespace Veda.Application.UseCases.AdminUseCases;

public record ListAllCustomersRequest : IRequest<IEnumerable<Customer>>;

public class ListAllCustomersRequestHandler(
    ICustomerRepository customerRepository,
    ILogger<ListAllCustomersRequestHandler> logger) : IRequestHandler<ListAllCustomersRequest, IEnumerable<Customer>>
{
    public Task<IEnumerable<Customer>> Handle(ListAllCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = customerRepository.GetAllIncludingAll();
        return Task.FromResult(customers);
    }
}