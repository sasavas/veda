using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record AddDigitalContentCommand(int customerId, int recipientId, string fileName, Stream fileStream) : IRequest;

public class AddDigitalContentCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<AddDigitalContentCommandHandler> logger)
    : IRequestHandler<AddDigitalContentCommand>
{
    public Task Handle(AddDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var recipientRepository = unitOfWork.GetRepository<Recipient>();

        var recipient = recipientRepository.GetById(command.recipientId)
                        ?? throw new NotFoundException(nameof(Recipient));
        
        //TODO generate hash code of the file
        var hashcode = "ABC1230332&*@)$I" + recipient.TCKimlikNo;
        //TODO calculate the actual length when saved to File Storage (in bytes, of course)
        var size = command.fileStream.Length;

        try
        {
            unitOfWork.BeginTransaction();

            recipient.AddContent(DigitalContent.Create(command.fileName, size, hashcode));

            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, "Could not add digital content to the recipient's folder");
            unitOfWork.Rollback();
            throw;
        }
        finally
        {
            unitOfWork.Dispose();
        }

        return Task.CompletedTask;
    }
}