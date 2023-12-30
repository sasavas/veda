using Veda.Application.Abstract;
using Veda.Application.Modules.CustomerModule.Exceptions;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.Modules.CustomerModule.Models;

public class Customer : Entity
{
    protected Customer(){}
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public TCKimlikNo TCKimlikNo { get; set; }
    public EmailAddress EmailAddress { get; set; }
    public PhoneNumber? PhoneNumber { get; set; }
    public Password Password { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();
    public Membership? ActiveMembership => Memberships.FirstOrDefault(m => m.Active);

    public virtual ICollection<Recipient> Recipients { get; private set; } = new List<Recipient>();

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
            Password = password
        };
    }

    public bool IsMaxRecipientCapacityReached()
    {
        var activeMembership = ActiveMembership;
        
        if(activeMembership is null)
        {
            throw new CustomerDoesNotHaveActiveMembership("Customer does not have an active membership");
        }

        return activeMembership.MembershipStatus.RecipientLimit == Recipients.Count;
    }

    public long GetTotalStorageUsed()
    {
        var currentTotalSize = Recipients
            .Select(r => r.Folder)
            .SelectMany(f => f.DigitalContents)
            .Sum(d => d.SizeInBytes);

        return currentTotalSize;
    }

    public void AddOrChangeMembership(MembershipStatus membershipStatus)
    {
        if (ActiveMembership?.MembershipStatus.Equals(membershipStatus) ?? false)
        {
            throw new DomainException("Customer already is on this subscription level");
        }
        
        if (Memberships.Any(m => m.Active))
        {
            var membership = Memberships.First(m => m.Active);
            membership.EndMembership();
        }

        Memberships.Add(Membership.Create(membershipStatus));
    }

    public void DeactiveCustomerAccount()
    {
        ActiveMembership?.EndMembership();
    }
}