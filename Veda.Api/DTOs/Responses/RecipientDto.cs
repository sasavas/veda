using Veda.Application.Modules.RecipientModule.Models;

namespace Veda.Api.DTOs.Responses;


public record RecipientDto(
    int Id,
    int CustomerId,
    string FirstName,
    string LastName,
    DateOnly dataOfBirth,
    string tcKimlikNo,
    string emailAddress,
    string phoneNumber,
    long CapacityTaken
)
{
    public static RecipientDto FromRecipient(Recipient recipient)
    {
        return new RecipientDto(recipient.Id,
            recipient.CustomerId, 
            recipient.FirstName, 
            recipient.LastName, 
            recipient.DateOfBirth, 
            recipient.TCKimlikNo.Value, 
            recipient.EMailAddress.Value,
            recipient.PhoneNumber.CountryCode + recipient.PhoneNumber.Number, 
            recipient.TotalCapacityOccupied());
    }
}