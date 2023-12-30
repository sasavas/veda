using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Exceptions;
using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Application.UseCases.VaultUseCases;

public record ClearCustomerContentsCommand(int CustomerId) : IRequest;

public class ClearCustomerContentsCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    IRecipientRepository recipientRepository,
    ILogger<ClearCustomerContentsCommandHandler> logger) : IRequestHandler<ClearCustomerContentsCommand>
{
    public Task Handle(ClearCustomerContentsCommand command, CancellationToken cancellationToken)
    {
        var recipients = customerRepository
            .GetByIdIncludingRecipientsAndContens(command.CustomerId)
            ?.Recipients ?? new List<Recipient>();

        foreach (var recipient in recipients)
        {
            recipient.ClearContents(DateTime.UtcNow);
        }

        unitOfWork.BeginTransaction();
        try
        {
            foreach (var recipient in recipients)
            {
                recipientRepository.Update(recipient);
            }
            
            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            
            logger.Log(LogLevel.Error, e, "Could not clear contents");
            throw new VedaApplicationException("Could not clear contents", e);
        }
        
        return Task.CompletedTask;
    }
}