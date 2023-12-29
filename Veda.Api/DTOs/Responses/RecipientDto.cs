namespace Veda.Api.DTOs.Responses;


public record RecipientDto(
    int CustomerId,
    string FirstName,
    string LastName, 
    DateOnly dataOfBirth,
    string tcKimlikNo,
    string emailAddress,
    string phoneNumber,
    long CapacityTaken
);