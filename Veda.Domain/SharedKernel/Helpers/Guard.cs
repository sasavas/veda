namespace Veda.Application.SharedKernel.Helpers;

public class Guard(object Obj, string ObjectName)
{
    public static Guard For(object Obj, string ObjectName)
    {
        return new Guard(Obj, ObjectName);
    }

    public Guard AgainstNull()
    {
        if (Obj == null)
        {
            throw new ArgumentNullException(ObjectName);
        }

        return this;
    }

    public Guard HasFixedLength(int length)
    {
        ThrowInvalidOperaionForNonStringParam(nameof(HasFixedLength));
        
        if (Obj is string objStr && objStr.Length != length)
        {
            throw new ArgumentException($"must have exactly {length} charachters", ObjectName);
        }

        return this;
    }

    public Guard HasAtLeastChars(int length)
    {
        ThrowInvalidOperaionForNonStringParam(nameof(HasAtLeastChars));
        
        if (Obj is string objStr && objStr.Length < length)
        {
            throw new ArgumentException($"must have exactly {length} charachters", ObjectName);
        }

        return this;
    }

    private void ThrowInvalidOperaionForNonStringParam(string nameofMethod)
    {
        if (Obj is not string)
        {
            throw new InvalidOperationException($"{nameofMethod} can only be applied to string variables");
        }
    }
}