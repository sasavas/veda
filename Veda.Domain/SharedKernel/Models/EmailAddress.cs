using Veda.Application.Abstract;

namespace Veda.Application.SharedKernel.Models;

public class EmailAddress : ValueObject
{
    public EmailAddress(string address)
    {
        //TODO validate
        Address = address;
    }

    public string Address { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not EmailAddress || obj is not string)
        {
            return false;
        }

        return obj switch
        {
            string => Address.Equals(obj),
            EmailAddress emailAddress => Equals(emailAddress),
            _ => false
        };
    }

    protected bool Equals(EmailAddress other)
    {
        return Address == other.Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }
}