using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.UseCases.CustomerUseCases;

public record StartCustomerSubscriptionCommand(int CustomerId, int MembershipStatusId) : IRequest;

public class StartCustomerSubscriptionCommandHandler(
    IRepository<Customer> customerRepository,
    IRepository<MembershipStatus> membershipStatusRepository) 
    : IRequestHandler<StartCustomerSubscriptionCommand>
{
    public Task Handle(StartCustomerSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var userId = customerRepository.GetById(request.CustomerId);
        var membership = membershipStatusRepository.GetById(request.MembershipStatusId);

        return Task.CompletedTask;
    }
}