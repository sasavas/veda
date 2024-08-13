using Veda.Infrastructure.ServiceImplementations;
using Veda.IntegrationTest.Abstract;
using Xunit.Abstractions;

namespace Veda.IntegrationTest.Tests;

public class FileEncryptionDecryptionTest(
    TestWebApplicationFactory factory,
    ITestOutputHelper testOutputHelper
)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public void DecryptEncryptedTextReturnsTheOriginalText()
    {
        // Arrange
        var fileEncryptor = new CustomFileEncryptor();

        // Load the original file
        using var fileStream = File.OpenRead("/Users/usavas/projects/_deployed/Veda/testdigitalcontent.txt");
        using var originalMemoryStream = new MemoryStream();
        fileStream.CopyTo(originalMemoryStream);
        var originalText = System.Text.Encoding.UTF8.GetString(originalMemoryStream.ToArray());
        
        testOutputHelper.WriteLine(originalText);

        // Act
        var encryptedData = fileEncryptor.Encrypt(new MemoryStream(originalMemoryStream.ToArray()));
        var decryptedStream = fileEncryptor.DecryptCryptoStream(
            new MemoryStream(encryptedData.encryptedFileContent.ToArray()), encryptedData.encryptedEncryptionKey);

        // Ensure the memory stream is positioned at the beginning before reading
        decryptedStream.Position = 0;
        var decryptedText = new StreamReader(decryptedStream).ReadToEnd();

        // Assert
        Assert.NotNull(decryptedText);
        Assert.Equal(originalText, decryptedText);
        
        testOutputHelper.WriteLine(decryptedText);
    }
}