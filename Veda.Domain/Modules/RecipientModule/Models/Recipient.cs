using System.Runtime.Versioning;
using Veda.Application.Abstract;
using Veda.Application.SharedKernel.Helpers;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.Modules.RecipientModule.Models;

public class Recipient : Entity
{
    private Recipient()
    {
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public TCKimlikNo TCKimlikNo { get; set; }
    public EmailAddress EMailAddress { get; set; }
    public PhoneNumber PhoneNumber { get; set; }

    public Folder Folder { get; set; }

    public static Recipient Create(string firstName, string lastName, string tcKimlikNo, string emailAddress,
        string phoneNumberCountryCode, long phoneNumber, DateOnly dateOfBirth)
    {
        Guard.For(firstName, nameof(firstName)).AgainstNull();
        Guard.For(lastName, nameof(lastName)).AgainstNull();

        var uniqueFolderName = $"{firstName.ToUpper()[0]}{lastName.ToUpper()[0]}_{Guid.NewGuid().ToString()}";
        return new Recipient()
        {
            FirstName = firstName,
            LastName = lastName,
            TCKimlikNo = new TCKimlikNo(tcKimlikNo),
            EMailAddress = new EmailAddress(emailAddress),
            PhoneNumber = new PhoneNumber(phoneNumberCountryCode, phoneNumber),
            DateOfBirth = dateOfBirth,
            Folder = Folder.Create(uniqueFolderName)
        };
    }
}