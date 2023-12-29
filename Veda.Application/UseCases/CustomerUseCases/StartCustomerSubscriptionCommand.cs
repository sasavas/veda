using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.CustomerUseCases;

public record StartCustomerSubscriptionCommand(int CustomerId, int MembershipStatusId) : IRequest<MembershipStatus>;

public class StartCustomerSubscriptionCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<StartCustomerSubscriptionCommand> logger) 
    : IRequestHandler<StartCustomerSubscriptionCommand, MembershipStatus>
{
    public Task<MembershipStatus> Handle(StartCustomerSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var customerRepository = unitOfWork.GetRepository<Customer>();
        var membershipStatusRepository = unitOfWork.GetRepository<MembershipStatus>();
        
        var customer = customerRepository.GetById(request.CustomerId);
        if (customer == null)
        {
            throw new NotFoundException(nameof(customer));
        }
        
        var membershipStatus = membershipStatusRepository.GetUnique(m => m.Id == request.MembershipStatusId);
        if (membershipStatus == null)
        {
            throw new NotFoundException(nameof(MembershipStatus));
        }
        
        customer.AddOrChangeMembership(Membership.Create(membershipStatus));

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