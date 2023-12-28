using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class Membership : Entity
{
    protected Membership(){}
    
    public virtual MembershipStatus MembershipStatus { get; set; }
    
    public bool Active => End == null;
    
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }

    public static Membership Create(MembershipStatus status)
    {
        return new Membership()
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