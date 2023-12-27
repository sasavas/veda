namespace Veda.Application.Modules.CustomerModule.Models;

public class Password
{
    public Password(string value)
    {
        //TODO validate
        
        //TODO hash password
        Value = value;
    }

    public string Value { get; }
    
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