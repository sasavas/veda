using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Infrastructure.DataAccess.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    protected CustomerRepository(VedaDbContext context) : base(context)
    {
    }
}