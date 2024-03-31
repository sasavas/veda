using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DomainServices;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.Ports.Storage;
using Veda.Application.Ports.Storage.Paths;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.AdminUseCases;

public record DeactivateCustomerCommand(int CustomerId) : IRequest;

public class DeleteCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    IStorageAccessorFactory storageAccessorFactory,
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
            
            var storageAccessor = storageAccessorFactory.Generate(new CustomerPath(customer));
            storageAccessor.DeleteFolder("");
            
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