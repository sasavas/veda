using System.Security.Cryptography;

namespace Veda.Application.Ports.Storage.Hashing;

public class FileHasher : IFileHasher
{
    // ReSharper disable once InconsistentNaming
    public string ComputeSHA256(Stream stream)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }
        
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "");
    }
}