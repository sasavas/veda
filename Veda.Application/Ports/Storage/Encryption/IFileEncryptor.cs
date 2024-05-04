namespace Veda.Application.Ports.Storage.Encryption;

public interface IFileEncryptor
{
    public MemoryStream Encrypt(Stream inputData, string passPhrase);
}