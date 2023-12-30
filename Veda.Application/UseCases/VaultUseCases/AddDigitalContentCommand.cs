using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.DomainServices;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record AddDigitalContentCommand(int recipientId, string fileName, Stream fileStream) : IRequest;

public class AddDigitalContentCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    IRecipientRepository recipientRepository,
    ILogger<AddDigitalContentCommandHandler> logger)
    : IRequestHandler<AddDigitalContentCommand>
{
    public Task Handle(AddDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var recipient = recipientRepository.GetByIdIncludingAllDigitalContent(command.recipientId)
                        ?? throw new NotFoundException(nameof(Recipient));
        var customer = customerRepository.GetByIdIncludingRecipientsAndContens(recipient.CustomerId)
                       ?? throw new NotFoundException(nameof(Customer));

        //TODO calculate the actual length when saved to File Storage (in bytes, of course)
        var size = command.fileStream?.Length ?? 1_000;

        var (canAdd, message) = DigitalContentService.CanAddDigitalContent(customer, size);
        if (canAdd == false)
        {
            throw new DomainException(message);
        }

        //TODO generate hash code of the file
        var hashcode = "ABC1230332&*@)$I" + recipient.TCKimlikNo.Value;

        recipient.AddContent(DigitalContent.Create(command.fileName, ".ogg", size, hashcode, DateTime.UtcNow));

        try
        {
            unitOfWork.BeginTransaction();

            recipientRepository.Update(recipient);

            //TODO: save the actual file to file storage API

            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, e, "Could not add digital content to the recipient's folder");
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