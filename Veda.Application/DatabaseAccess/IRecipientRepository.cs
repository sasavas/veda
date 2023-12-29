using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Application.DatabaseAccess;

public interface IRecipientRepository : IRepository<Recipient>
{
    Recipient? GetByIdIncludingAllDigitalContent(int id);
}