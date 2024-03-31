using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Application.Ports.DataAccess;

public interface IRecipientRepository : IRepository<Recipient>
{
    Recipient? GetByIdIncludingAllDigitalContent(int id);
}