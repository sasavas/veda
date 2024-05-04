using Veda.Infrastructure.ServiceImplementations;
using Xunit.Abstractions;

namespace Veda.IntegrationTest.Tests;

public class PgpEncryptionTest(ITestOutputHelper testOutputHelper)
{
    private ITestOutputHelper _testOutputHelper = testOutputHelper;

    [Fact]
    public void DecryptEncryptedTextReturnsTheOriginalText()
    {
        const string publicKeyStringIsThisOk = "public_key_string_is_this_OK";
        
        var fileEncryptor = new PgpFileEncryptor();
        
        var encrypted = fileEncryptor.Encrypt(new MemoryStream(new byte[]{1, 2}), publicKeyStringIsThisOk);
        
        Assert.True(encrypted.Length > 0);
        OutputMemoryStream(encrypted, 10);

        var decrypted = fileEncryptor.DecryptData(encrypted, publicKeyStringIsThisOk);
        
        Assert.True(decrypted.ToArray()[0].ToString() == "1");
        OutputMemoryStream(decrypted, 2);
    }

    private void OutputMemoryStream(MemoryStream stream, int count)
    {
        var arr = stream.ToArray();
        for (int i = 0; i < count; i++)
        {
            _testOutputHelper.WriteLine(arr[i].ToString());
        }
    }
}