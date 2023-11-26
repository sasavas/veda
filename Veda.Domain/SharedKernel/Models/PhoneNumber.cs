namespace Veda.Application.SharedKernel.Models;

public class PhoneNumber(string countryCode, string number)
{
    public string CountryCode { get; } = countryCode;
    public string Number { get; } = number;
}