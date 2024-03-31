using Veda.Application.Ports;

namespace Veda.Infrastructure.ServiceImplementations;

public class DummyFileHasher : IFileHasher
{
    public string Generate(Stream stream, string tcKimlikNo)
    {
        return stream.GetHashCode() + tcKimlikNo;
    }
}