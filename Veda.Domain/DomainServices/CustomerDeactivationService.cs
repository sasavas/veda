using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.DomainServices;

public static class CustomerDeactivationService
{
    public static void DeactivateCustomerAccount(Customer customer)
    {
        customer.DeactiveCustomerAccount();

        foreach (var recipient in customer.Recipients)
        {
            recipient.DeactivateRecipient(DateTime.UtcNow);
        }
    }
}