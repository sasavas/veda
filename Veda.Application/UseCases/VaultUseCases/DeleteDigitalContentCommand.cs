using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record DeleteDigitalContentCommand(int RecipientId, int ContentId) : IRequest;

public class DeleteDigitalContentCommandHandler(
    IUnitOfWork unitOfWork,
    IRecipientRepository recipientRepository,
    ILogger<DeleteDigitalContentCommandHandler> logger)
    : IRequestHandler<DeleteDigitalContentCommand>
{
    public Task Handle(DeleteDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var digitalContentRepository = unitOfWork.GetRepository<DigitalContent>();

        var recipient = recipientRepository.GetByIdIncludingAllDigitalContent(command.RecipientId)
                        ?? throw new NotFoundException(nameof(Recipient));

        var content = digitalContentRepository.GetById(command.ContentId)
                      ?? throw new NotFoundException(nameof(DigitalContent));

        unitOfWork.BeginTransaction();
        try
        {
            recipient.DeleteContent(content, DateTime.UtcNow);
            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            logger.Log(LogLevel.Error, e, "Error occurred while deleting the content");
            throw;
        }
        finally
        {
            unitOfWork.Dispose();
        }

        return Task.CompletedTask;
    }
}