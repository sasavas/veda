using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;
using Xunit.Abstractions;

namespace Veda.Domain.Tests;

public class CustomerTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void CustomerCanHaveOnlyOneActiveMembershipAtOnce()
    {
        var customer = new Customer()
        {
            FirstName = "Ahmet",
            LastName = "Birgili",
            TCKimlikNo = new TCKimlikNo("11111111111"),
            Password = new Password("1111"),
            EmailAddress = new EmailAddress("metinbirgili@yahoo.com"),
        };

        var memberShip = MemberShip.Create(
            new MembershipStatus
            {
                DigitalStorageCapacityInBytes = 2_000_000_000,
                RecipientLimit = 2,
                StatusName = "???"
            });

        customer.AddOrChangeMembership(memberShip);

        Assert.True(customer.ActiveMemberShip.Active);
        Assert.True(customer.ActiveMemberShip.End == default);

        var activeMembership = customer.ActiveMemberShip;

        customer.AddOrChangeMembership(
            MemberShip.Create(
                new MembershipStatus
                {
                    DigitalStorageCapacityInBytes = 2_000_000_000,
                    RecipientLimit = 2,
                    StatusName = "???"
                }));
        
        testOutputHelper.WriteLine(activeMembership.End.ToString());
        Assert.True(activeMembership.End != default);
        Assert.True(customer.Memberships.Count(m => m.Active) == 1);
    }
}