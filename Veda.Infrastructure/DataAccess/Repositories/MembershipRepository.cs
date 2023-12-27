using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Infrastructure.DataAccess.Repositories;

public class MembershipRepository : Repository<MemberShip>, IMembershipRepository
{
    protected MembershipRepository(VedaDbContext context) : base(context)
    {
    }
}