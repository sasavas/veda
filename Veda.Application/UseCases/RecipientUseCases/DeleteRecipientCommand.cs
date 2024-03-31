using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.RecipientUseCases;

public record DeleteRecipientCommand(int RecipientId) : IRequest;

public class RemoveRecipientCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<RemoveRecipientCommandHandler> logger)
    : IRequestHandler<DeleteRecipientCommand>
{
    public Task Handle(DeleteRecipientCommand request, CancellationToken cancellationToken)
    {
        var recipientRepository = unitOfWork.GetRepository<Recipient>();

        var recipient = recipientRepository.GetById(request.RecipientId)
                        ?? throw new NotFoundException(nameof(Recipient));

        unitOfWork.BeginTransaction();
        try
        {
            recipientRepository.Delete(recipient.Id);

            unitOfWork.Commit();

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            logger.Log(LogLevel.Error, e, "Error while creating Recipient");
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}