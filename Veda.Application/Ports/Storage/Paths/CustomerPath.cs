using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Application.Ports.Storage.Paths;

public class CustomerPath : RelativePath
{
    public override string Value { get; }

    public CustomerPath(Customer customer)
    {
        Value = customer.Id.ToString();
    }
}