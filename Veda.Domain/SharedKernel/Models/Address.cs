using Veda.Application.Abstract;

namespace Veda.Application.SharedKernel.Models;

public class Address : ValueObject
{
    public string Title { get; set; }
    public string AddressField { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
}