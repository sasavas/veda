using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class MembershipStatus : Entity
{
    private MembershipStatus()
    {
    }

    public static MembershipStatus Create(string statusName, long digitalStorageCapacityInBytes, int recipientLimit)
    {
        return new MembershipStatus()
        {
            StatusName = statusName,
            DigitalStorageCapacityInBytes = digitalStorageCapacityInBytes,
            RecipientLimit = recipientLimit
        };
    }

    public string StatusName { get; set; }
    public long DigitalStorageCapacityInBytes { get; set; }

    /// <summary>
    /// How many recipient a Customer can have
    /// </summary>
    public int RecipientLimit { get; set; }
}