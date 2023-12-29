using Veda.Application.Abstract;

namespace Veda.Application.SharedKernel.Models;

public class EmailAddress : ValueObject
{
    public EmailAddress(string value)
    {
        //TODO validate
        Value = value;
    }

    public string Value { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not EmailAddress || obj is not string)
        {
            return false;
        }

        return obj switch
        {
            string => Value.Equals(obj),
            EmailAddress emailAddress => Equals(emailAddress),
            _ => false
        };
    }

    protected bool Equals(EmailAddress other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}