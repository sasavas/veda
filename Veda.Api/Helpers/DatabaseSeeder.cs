using Veda.Application.Modules.CustomerModule.Models;
using Veda.Infrastructure.DataAccess;

namespace Veda.Api.Helpers;

public static class DatabaseSeeder
{
    public static void Seed(VedaDbContext context)
    {
        var memberships = context.Set<Membership>();
        
        memberships.AddRange(
            new List<Membership>
            {
                Membership.Create(MembershipStatus.Create("Basic", 2_000_000_000, 2)),
                Membership.Create(MembershipStatus.Create("Premium", 5_000_000_000, 5)),
                Membership.Create(MembershipStatus.Create("Gold", 10_000_000_000, 10)),
            });

        context.SaveChanges();
    }
}