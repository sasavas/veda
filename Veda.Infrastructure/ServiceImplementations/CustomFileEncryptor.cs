using System.Security.Cryptography;
using Veda.Application.Ports.Storage.Encryption;

namespace Veda.Infrastructure.ServiceImplementations;

public class CustomFileEncryptor : IFileEncryptor
{
    public FileEncryptionResult Encrypt(Stream fileStream)
    {
        using var aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();

        using var fileOutput = new MemoryStream();

        // Write the IV to the file first (needed for decryption)
        fileOutput.Write(aes.IV, 0, aes.IV.Length);

        using var cryptoStream = new CryptoStream(fileOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);
        fileStream.CopyTo(cryptoStream);

        cryptoStream.FlushFinalBlock(); // Ensure all data is written

        // Encrypt the AES key with your public key
        byte[] encryptedKey = EncryptKeyWithPublicKey(aes.Key);

        // Return the encrypted stream and encrypted AES key
        return new FileEncryptionResult(new MemoryStream(fileOutput.ToArray()), encryptedKey);
    }

    private static byte[] EncryptKeyWithPublicKey(byte[] key)
    {
        using var rsa = RSA.Create();
        // Assume you load your public key here
        rsa.ImportFromPem(File.ReadAllText("/Users/usavas/projects/_deployed/Veda/publicKey.pem"));
        return rsa.Encrypt(key, RSAEncryptionPadding.Pkcs1);
    }
    
    public MemoryStream DecryptCryptoStream(Stream fileStream, byte[] encryptedKey)
    {
        // Decrypt the contents of the CryptoStream using AES
        using var aes = Aes.Create();
        aes.Key = DecryptKeyWithPrivateKey(encryptedKey);

        // Read the IV from the beginning of the CryptoStream
        byte[] iv = new byte[16]; // AES block size is 16 bytes
        fileStream.Read(iv, 0, iv.Length);
        aes.IV = iv;

        // Create a temporary MemoryStream to hold the decrypted data
        var memoryStream = new MemoryStream();

        using var decryptStream = new CryptoStream(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        decryptStream.CopyTo(memoryStream);

        return memoryStream;
    }

    private static byte[] DecryptKeyWithPrivateKey(byte[] encryptedKey)
    {
        using var rsa = RSA.Create();
        string privateKeyPem = File.ReadAllText("/Users/usavas/projects/_deployed/Veda/privateKey.pem");
        
        const string passphrase = "1460";

        // Import the encrypted private key using the provided passphrase
        rsa.ImportFromEncryptedPem(privateKeyPem, passphrase.AsSpan());
        return rsa.Decrypt(encryptedKey, RSAEncryptionPadding.Pkcs1);
    }

}