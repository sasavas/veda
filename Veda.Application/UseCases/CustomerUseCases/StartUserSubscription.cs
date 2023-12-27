using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.CustomerUseCases;

public record StartCustomerSubscriptionCommand(int CustomerId, int MembershipStatusId) : IRequest;

public class StartCustomerSubscriptionCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<StartCustomerSubscriptionCommand> logger) 
    : IRequestHandler<StartCustomerSubscriptionCommand>
{
    public Task Handle(StartCustomerSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var customerRepository = unitOfWork.GetRepository<Customer>();
        var membershipRepository = unitOfWork.GetRepository<Membership>();
        
        var customer = customerRepository.GetById(request.CustomerId);
        if (customer == null)
        {
            throw new NotFoundException<Customer>();
        }
        
        var membership = membershipRepository.GetUnique(m => m.MembershipStatus.Id == request.MembershipStatusId);
        if (membership == null)
        {
            throw new NotFoundException<Membership>();
        }
        
        customer.AddOrChangeMembership(membership);

        unitOfWork.BeginTransaction();
        try
        {
            customerRepository.Update(customer);
            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            logger.Log(LogLevel.Error, e, "Could not start customer membership");
            
            throw;
        }
        
        return Task.CompletedTask;
    }
}