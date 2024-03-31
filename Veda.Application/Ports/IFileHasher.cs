namespace Veda.Application.Ports;

public interface IFileHasher
{
    string Generate(Stream stream, string tcKimlikNo);
}