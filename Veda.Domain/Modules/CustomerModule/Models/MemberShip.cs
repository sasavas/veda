using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class MemberShip : Entity
{
    public MembershipStatus MembershipStatus { get; set; }

    public bool Active => (DateTime.Now - End).TotalHours > 0;

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}