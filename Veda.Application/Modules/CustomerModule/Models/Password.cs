namespace Veda.Application.Modules.CustomerModule.Models;

public class Password
{
    public Password(string value)
    {
        //TODO validate
        
        //TODO hash password
        Value = value;
    }

    public string Value { get; set; }
}