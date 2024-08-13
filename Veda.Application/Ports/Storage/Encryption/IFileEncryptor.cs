using System.Security.Cryptography;

namespace Veda.Application.Ports.Storage.Encryption;

/// <summary>
/// 
/// </summary>
/// <param name="encryptedFileContent"></param>
/// <param name="encryptedEncryptionKey">The key used to encrypt the file encryption key.
/// Secure it in the database. Which will be required to decrypt the Aes key before decrypting the file itself</param>
public record FileEncryptionResult(
    MemoryStream encryptedFileContent,
    byte[] encryptedEncryptionKey
);

public interface IFileEncryptor
{
    FileEncryptionResult Encrypt(Stream inputData);

    MemoryStream DecryptCryptoStream(Stream fileStream, byte[] encryptedKey);
}