using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.DomainServices;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.AdminUseCases;

public record DeactivateCustomerCommand(int CustomerId) : IRequest;

public class DeleteCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    ILogger<DeleteCustomerCommandHandler> logger)
    : IRequestHandler<DeactivateCustomerCommand>
{
    public Task Handle(DeactivateCustomerCommand command, CancellationToken cancellationToken)
    {
        unitOfWork.BeginTransaction();

        try
        {
            var customer = customerRepository.GetByIdIncludingRecipientsAndContens(command.CustomerId) 
                           ?? throw new NotFoundException(nameof(Customer));
            
            CustomerDeactivationService.DeactivateCustomerAccount(customer);
            
            //TODO: delete actual files
            
            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            
            logger.Log(LogLevel.Error, e, "Could not delete the Customer");
            throw;
        }

        return Task.CompletedTask;
    }
}