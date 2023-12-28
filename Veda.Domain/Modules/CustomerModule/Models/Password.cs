using Veda.Application.Abstract;

namespace Veda.Application.Modules.CustomerModule.Models;

public class Password : ValueObject
{
    public static Password Create(string value)
    {
        return new Password { Value = value };
    }

    public string Value { get; private init; }

    public override bool Equals(object? obj)
    {
        if (obj is not Password || obj is not string)
        {
            return false;
        }

        return obj switch
        {
            string => Value.Equals(obj),
            Password password => Equals(password),
            _ => false
        };
    }

    protected bool Equals(Password other)
    {
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}