using Microsoft.EntityFrameworkCore;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Infrastructure.DataAccess.RepositoryAdapters;

public class RecipientRepository(VedaDbContext context) : Repository<Recipient>(context), IRecipientRepository
{
    public Recipient? GetByIdIncludingAllDigitalContent(int id)
    {
        return Context.Set<Recipient>()
            .Include(recipient => recipient.Folder)
            .ThenInclude(folder => folder.DigitalContents.Where(content => content.DeletionDate == null))
            .FirstOrDefault(r => r.Id == id);
    }
}