using Veda.Application.Modules.CustomerModule.Models;

namespace Veda.Api.DTOs.Responses;

public record CustomerDto(
    int Id,
    string FirstName,
    string LastName,
    DateOnly dataOfBirth,
    string tcKimlikNo,
    string emailAddress,
    string phoneNumber,
    DateTime? memberSince,
    string? activeMembership,
    IEnumerable<RecipientDto> Recipients,
    int CurrentRecipientCount,
    long CapacityTaken)
{
    public static CustomerDto FromCustomerEntity(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth,
            customer.TCKimlikNo.Value,
            customer.EmailAddress.Value,
            customer.PhoneNumber?.FullNumber ?? string.Empty,
            customer.ActiveMembership?.Start,
            customer.ActiveMembership?.MembershipStatus.StatusName ?? string.Empty,
            customer.Recipients.Select(RecipientDto.FromRecipient),
            customer.Recipients.Count(recipient => recipient.DeactivationDate is null),
            customer.Recipients.Sum(recipient => recipient.TotalCapacityOccupied()));
    }
}