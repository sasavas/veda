using Veda.Application.Abstract;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.Modules.CustomerModule.Models;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public TCKimlikNo TCKimlikNo { get; set; }
    public EmailAddress EmailAddress { get; set; }
    public Password Password { get; set; }

    public List<MemberShip> Memberships { get; set; }
    public MemberShip ActiveMemberShip => Memberships.FirstOrDefault(m => m.Active);


    public ICollection<int> RecipientIds { get; set; }
}