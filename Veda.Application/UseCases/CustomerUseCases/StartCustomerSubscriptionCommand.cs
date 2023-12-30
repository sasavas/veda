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
        var membershipStatusRepository = unitOfWork.GetRepository<MembershipStatus>();
        
        var customer = customerRepository.GetById(request.CustomerId);
        if (customer is null)
        {
            throw new NotFoundException(nameof(customer));
        }
        
        var membershipStatus = membershipStatusRepository.GetUnique(m => m.Id == request.MembershipStatusId);
        if (membershipStatus is null)
        {
            throw new NotFoundException(nameof(MembershipStatus));
        }
        
        customer.AddOrChangeMembership(membershipStatus);

        try
        {
            unitOfWork.BeginTransaction();
            customerRepository.Update(customer);
            unitOfWork.Commit();
            return Task.FromResult(membershipStatus);
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            logger.Log(LogLevel.Error, e, "Could not start customer membership");
            throw;
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}