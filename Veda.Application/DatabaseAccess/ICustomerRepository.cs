using System.Linq.Expressions;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DatabaseAccess;

public interface ICustomerRepository : IRepository<Customer>
{
    IEnumerable<Customer> GetAllIncludingAll();

    Customer? GetByIdIncludingRecipients(int id);

    Customer? GetByIdIncludingRecipientsAndContens(int id);
}