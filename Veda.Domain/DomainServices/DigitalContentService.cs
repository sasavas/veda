using Veda.Application.Modules.CustomerModule.Exceptions;
using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DomainServices;

public static class DigitalContentService
{
    public static (bool, string)  CanAddDigitalContent(Customer customer, long contentSize)
    {
        var activeMembership = customer.ActiveMembership;

        if (activeMembership is null)
        {
            throw new CustomerDoesNotHaveActiveMembership("Customer does not have an active membership");
        }

        var currentStorageInUse = customer.GetTotalStorageUsed();
        
        if (currentStorageInUse > activeMembership.MembershipStatus.DigitalStorageCapacityInBytes )
        {
            return (false, "Customer's current capacity is full");
        }

        if ((currentStorageInUse + contentSize)  > activeMembership.MembershipStatus.DigitalStorageCapacityInBytes)
        {
            return (false, "Customer's folder does not have enough capacity for this file");
        }

        return (true, string.Empty);
    }
}