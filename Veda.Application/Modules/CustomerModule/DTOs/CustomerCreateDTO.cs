namespace Veda.Application.Modules.CustomerModule.DTOs;

public record CustomerCreateDTO(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string TCKimlikNo,
    string EmailAddress,
    string Password,
    int MembershipStatusId);