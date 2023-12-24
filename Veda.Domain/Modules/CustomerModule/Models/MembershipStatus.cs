using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class MembershipStatus : Entity
{
    public string StatusName { get; set; }
    public long DigitalStorageCapacityInBytes { get; set; }
    /// <summary>
    /// How many recipient a Customer can have
    /// </summary>
    public int RecipientLimit { get; set; }
}