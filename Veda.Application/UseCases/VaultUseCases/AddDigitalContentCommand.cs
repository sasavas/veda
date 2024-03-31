using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports;
using Veda.Application.Ports.DataAccess;
using Veda.Application.Ports.Storage;
using Veda.Application.Ports.Storage.Paths;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record AddDigitalContentCommand(int recipientId, string fileName, Stream fileStream) : IRequest;

public class AddDigitalContentCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    IRecipientRepository recipientRepository,
    IFileHasher fileHasher,
    IStorageAccessorFactory storageAccessorFactory,
    ILogger<AddDigitalContentCommandHandler> logger)
    : IRequestHandler<AddDigitalContentCommand>
{
    public Task Handle(AddDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var recipient = recipientRepository.GetByIdIncludingAllDigitalContent(command.recipientId)
                        ?? throw new NotFoundException(nameof(Recipient));
        var customer = customerRepository.GetByIdIncludingRecipientsAndContens(recipient.CustomerId)
                       ?? throw new NotFoundException(nameof(Customer));

        var size = command.fileStream.Length;

        var (canAdd, message) = customer.CanAddDigitalContent(size);
        if (canAdd == false)
        {
            throw new DomainException(message);
        }

        var hashcode = fileHasher.Generate(command.fileStream, recipient.TCKimlikNo.Value);

        recipient.AddContent(
            DigitalContent.Create(command.fileName, ".ogg", size, hashcode, DateTime.UtcNow));

        try
        {
            //TODO: consider transactional integrity
            
            var storageAccessor = storageAccessorFactory.Generate(new RecipientPath(customer, recipient));
            storageAccessor.UploadFile(command.fileStream, command.fileName);

            unitOfWork.BeginTransaction();
            recipientRepository.Update(recipient);
            unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not add digital content to the recipient's folder");
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