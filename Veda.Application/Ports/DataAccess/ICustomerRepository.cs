using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.Ports.DataAccess;

public interface ICustomerRepository : IRepository<Customer>
{
    IEnumerable<Customer> GetAllIncludingAll();

    Customer? GetByIdIncludingRecipients(int id);

    Customer? GetByIdIncludingRecipientsAndContens(int id);
}