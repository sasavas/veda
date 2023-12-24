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

    public List<MemberShip> Memberships { get; set; } = new();
    public MemberShip ActiveMemberShip => Memberships.FirstOrDefault(m => m.Active);

    public static Customer Create(string firstName, string lastName, DateOnly dateOfBirth,
        TCKimlikNo tcKimlikNo, EmailAddress emailAddress, Password password)
    {
        return new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            EmailAddress = emailAddress,
            TCKimlikNo = tcKimlikNo,
        };
    }

    public void AddOrChangeMembership(MemberShip memberShip)
    {
        if (Memberships.Any(m => m.Active))
        {
            var membership = Memberships.First(m => m.Active);
            membership.EndMembership();
        }

        Memberships.Add(memberShip);
    }

    public ICollection<int> RecipientIds { get; set; }
}