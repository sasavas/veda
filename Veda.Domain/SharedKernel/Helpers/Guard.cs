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

    public Guard HasLength(int length)
    {
        if (Obj is string objStr && objStr.Length != length)
        {
            throw new ArgumentException($"must have exactly {length} charachters", ObjectName);
        }

        return this;
    }
}