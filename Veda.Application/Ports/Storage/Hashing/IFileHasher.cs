namespace Veda.Application.Ports.Storage.Hashing;

public interface IFileHasher
{
    // ReSharper disable once InconsistentNaming
    string ComputeSHA256(Stream stream);
}