using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.Ports.Storage;
using Veda.Application.Ports.Storage.Encryption;
using Veda.Application.Ports.Storage.Paths;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.VaultUseCases;

public record AccessDigitalContentCommand(int RecipientId, int DigitalContentId) : IRequest<AccessDigitalContentResult>;

public record AccessDigitalContentResult(MemoryStream MemoryStream, string FileExtension);

public class AccessDigitalContentCommandHandler(
    ICustomerRepository customerRepository,
    IRecipientRepository recipientRepository,
    IStorageAccessorFactory storageAccessorFactory,
    ILogger<AddDigitalContentCommandHandler> logger,
    IFileEncryptor fileEncryptor
) : IRequestHandler<AccessDigitalContentCommand, AccessDigitalContentResult>
{
    public Task<AccessDigitalContentResult> Handle(AccessDigitalContentCommand command, CancellationToken cancellationToken)
    {
        var recipient = recipientRepository.GetByIdIncludingAllDigitalContent(command.RecipientId)
                        ?? throw new NotFoundException(nameof(Recipient));
        var customer = customerRepository.GetByIdIncludingRecipientsAndContens(recipient.CustomerId)
                       ?? throw new NotFoundException(nameof(Customer));

        var storageAccessor = storageAccessorFactory.Generate(new RecipientPath(customer, recipient));
        
        var content = recipient.GetContent(command.DigitalContentId);
        if (content is null)
        {
            throw new DigitalContentNotFoundException();
        }

        var fileStream = storageAccessor.DownloadFile(content.Name);
        
        var decrypted = fileEncryptor.DecryptCryptoStream(fileStream, content.EncryptedFileEncryptionKey);

        return Task.FromResult(new AccessDigitalContentResult(decrypted, content.FileExtension));
    }
}

public class DigitalContentNotFoundException : Exception
{
}