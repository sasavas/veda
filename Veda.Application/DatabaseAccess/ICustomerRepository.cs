using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DatabaseAccess;

public interface ICustomerRepository
{
    Customer Create(Customer customer);
}