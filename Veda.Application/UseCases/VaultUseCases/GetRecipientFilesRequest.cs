using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Application.UseCases.VaultUseCases;

public record GetRecipientFilesRequest(int RecipientId) : IRequest<IEnumerable<DigitalContent>>;

public class GetRecipientFilesRequestHandler(
    IRecipientRepository recipientRepository)
    : IRequestHandler<GetRecipientFilesRequest, IEnumerable<DigitalContent>>
{
    public Task<IEnumerable<DigitalContent>> Handle(GetRecipientFilesRequest request, CancellationToken cancellationToken)
    {
        var contents = recipientRepository
            .GetByIdIncludingAllDigitalContent(request.RecipientId)
            ?.Folder
            .DigitalContents
            .AsEnumerable() ?? Enumerable.Empty<DigitalContent>();

        return Task.FromResult(contents);
    }
}