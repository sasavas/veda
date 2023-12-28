using Veda.Application.Abstract;

namespace Veda.Application.SharedKernel.Models;

public class PhoneNumber(string countryCode, long number) : ValueObject
{
    public string CountryCode { get; private set; } = countryCode;
    public long Number { get; private set; } = number;
}