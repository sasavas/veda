using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.Modules.CustomerModule.DatabaseAccess;

public interface ICustomerRepository
{
    Customer Create(Customer customer);
}