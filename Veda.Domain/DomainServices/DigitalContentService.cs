using Veda.Application.Modules.CustomerModule.Exceptions;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DomainServices;

public static class DigitalContentService
{
    public static (bool, string)  CanAddDigitalContent(Customer customer, long contentSize)
    {
        var activeMembership = customer.ActiveMemberShip;

        if (activeMembership is null)
        {
            throw new CustomerDoesNotHaveActiveMembership("Customer does not have an active membership");
        }

        var currentStorageInUse = customer.GetTotalStorageUsed();
        
        if (activeMembership.MembershipStatus.DigitalStorageCapacityInBytes >= currentStorageInUse)
        {
            return (false, "Customer's current capacity is full");
        }

        if (activeMembership.MembershipStatus.DigitalStorageCapacityInBytes >= (currentStorageInUse + contentSize))
        {
            return (false, "The content's size is too large");
        }

        return (true, string.Empty);
    }
}