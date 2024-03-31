using Veda.Application.Abstract;
using ValidationException = Veda.Application.SharedKernel.Exceptions.ValidationException;

namespace Veda.Application.SharedKernel.Models;

public class TCKimlikNo : ValueObject
{
    public TCKimlikNo(string value)
    {
        if (value.Length != 11)
        {
            throw new ValidationException("TCKimlikNo must be exactly 11 characters");
        }
        
        Value = value;
    }

    public string Value { get; set; }

    public override string ToString()
    {
        return Value;
    }
}