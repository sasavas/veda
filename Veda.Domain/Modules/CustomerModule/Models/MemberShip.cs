using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class MemberShip : Entity
{
    private MemberShip(){}
    
    public MembershipStatus MembershipStatus { get; set; }

    public bool Active => End == null;

    public DateTime Start { get; set; }
    public DateTime? End { get; set; }

    public static MemberShip Create(MembershipStatus status)
    {
        return new MemberShip()
        {
            Start = DateTime.UtcNow,
            MembershipStatus = status,
        };
    }

    public void EndMembership()
    {
        End = DateTime.UtcNow;
    }
}