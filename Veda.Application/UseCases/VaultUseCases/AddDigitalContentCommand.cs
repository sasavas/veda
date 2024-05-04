using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.Ports.Storage;
using Veda.Application.Ports.Storage.Encryption;
using Veda.Application.Ports.Storage.Hashing;
using Veda.Application.Ports.Storage.Paths;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record AddDigitalContentCommand(int RecipientId, string FileName, Stream FileStream) : IRequest;

public class AddDigitalContentCommandHandler(
    IUnitOfWork unitOfWork,
    ICustomerRepository customerRepository,
    IRecipientRepository recipientRepository,
    IFileEncryptor fileEncryptor,
    IFileHasher fileHasher,
    IStorageAccessorFactory storageAccessorFactory,
    ILogger<AddDigitalContentCommandHandler> logger)
    : IRequestHandler<AddDigitalContentCommand>
{
    public Task Handle(AddDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var recipient = recipientRepository.GetByIdIncludingAllDigitalContent(command.RecipientId)
                        ?? throw new NotFoundException(nameof(Recipient));
        var customer = customerRepository.GetByIdIncludingRecipientsAndContens(recipient.CustomerId)
                       ?? throw new NotFoundException(nameof(Customer));

        var size = command.FileStream.Length;

        var (canAdd, message) = customer.CanAddDigitalContent(size);
        if (canAdd == false)
        {
            throw new DomainException(message);
        }

        var encryptedFileStream = fileEncryptor.Encrypt(command.FileStream, recipient.TCKimlikNo.Value);
        var hashcode = fileHasher.ComputeSHA256(command.FileStream);
        recipient.AddContent(
            DigitalContent.Create(command.FileName, ".ogg", size, hashcode, DateTime.UtcNow));

        try
        {
            //TODO: consider transactional integrity
            
            var storageAccessor = storageAccessorFactory.Generate(new RecipientPath(customer, recipient));
            storageAccessor.UploadFile(command.FileStream, command.FileName);

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