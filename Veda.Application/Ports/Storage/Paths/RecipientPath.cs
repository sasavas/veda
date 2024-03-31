using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Application.Ports.Storage.Paths;

public class RecipientPath : RelativePath
{
    public override string Value { get; }

    public RecipientPath(Customer recipient, Recipient customer)
    {
        Value = Path.Combine("Customer" + customer.Id, "Recipient" + recipient.Id);
    }
}