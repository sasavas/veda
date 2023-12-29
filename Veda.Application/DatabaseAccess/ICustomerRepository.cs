using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DatabaseAccess;

public interface ICustomerRepository : IRepository<Customer>
{
    Customer? GetByIdIncludingRecipients(int id);

    Customer? GetByIdIncludingRecipientsAndContens(int id);
}