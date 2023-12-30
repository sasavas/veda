using Veda.Application.Abstract;
using Veda.Application.SharedKernel.Helpers;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.Modules.RecipientModule.Models;

public class Recipient : Entity
{
    protected Recipient()
    {
    }

    public int CustomerId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public TCKimlikNo TCKimlikNo { get; set; }
    public EmailAddress EMailAddress { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public DateTime? DeactivationDate { get; private set; }
    public virtual Folder Folder { get; set; }

    public static Recipient Create(int customerId, string firstName, string lastName, string tcKimlikNo, string emailAddress,
        string phoneNumberCountryCode, long phoneNumber, DateOnly dateOfBirth)
    {
        Guard.For(customerId, nameof(customerId)).AgainstNull();
        Guard.For(firstName, nameof(firstName)).AgainstNull();
        Guard.For(lastName, nameof(lastName)).AgainstNull();

        var uniqueFolderName = $"{firstName.ToUpper()[0]}{lastName.ToUpper()[0]}_{Guid.NewGuid().ToString()}";
        return new Recipient
        {
            CustomerId = customerId,
            FirstName = firstName,
            LastName = lastName,
            TCKimlikNo = new TCKimlikNo(tcKimlikNo),
            EMailAddress = new EmailAddress(emailAddress),
            PhoneNumber = new PhoneNumber(phoneNumberCountryCode, phoneNumber),
            DateOfBirth = dateOfBirth,
            Folder = Folder.Create(uniqueFolderName)
        };
    }

    public void DeactivateRecipient(DateTime deactivationDate)
    {
        DeactivationDate = deactivationDate;
        Folder.ClearContents(deactivationDate);
    }
        
    public void AddContent(DigitalContent content)
    {
        Folder.AddContent(content);
    }

    public void DeleteContent(DigitalContent content, DateTime deletionTime)
    {
        Folder.RemoveContent(content, deletionTime);
    }

    public void ClearContents(DateTime deletionTime)
    {
        Folder.ClearContents(deletionTime);
    }

    public long TotalCapacityOccupied() => 
        Folder.DigitalContents.Sum(content => content.SizeInBytes);
}