using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;
using Xunit.Abstractions;

namespace Veda.Domain.Tests;

public class CustomerTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void CustomerCanHaveOnlyOneActiveMembershipAtOnce()
    {
        var customer = Customer.Create(
            "Ahmet",
            "Birgili",
            new DateOnly(1992, 1, 1),
            new TCKimlikNo("11111111111"),
            new EmailAddress("ahmetin@yahoo.com"),
            Password.Create("1111"));

        var membershipStatus = MembershipStatus.Create(
            "???",
            2_000_000_000,
            2
        );

        customer.AddOrChangeMembership(membershipStatus);

        Assert.True(customer.ActiveMembership?.Active);
        Assert.True(customer.ActiveMembership?.End == default);

        var activeMembership = customer.ActiveMembership;

        var higherMembershipStatus = MembershipStatus.Create(
            "???",
            1_000_000_000,
            1);
        
        customer.AddOrChangeMembership(higherMembershipStatus);

        testOutputHelper.WriteLine(activeMembership.End.ToString());
        Assert.True(activeMembership.End != default);
        Assert.True(customer.Memberships.Count(m => m.Active) == 1);
    }
}