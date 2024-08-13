using Microsoft.Extensions.DependencyInjection;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
using Veda.Application.SharedKernel.Models;
using Veda.Application.UseCases.VaultUseCases;
using Veda.Infrastructure.DataAccess;
using Veda.IntegrationTest.Abstract;
using Xunit.Abstractions;

namespace Veda.IntegrationTest.Tests;

public class FileEncryptionDecryptionTest2 : BaseIntegrationTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly VedaDbContext _dbContext;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRecipientRepository _recipientRepository;

    public FileEncryptionDecryptionTest2(
        TestWebApplicationFactory factory, ITestOutputHelper outputHelper) : base(factory)
    {
        _testOutputHelper = outputHelper;
        _dbContext = TestWebApplicationFactory.ServiceProvider.GetRequiredService<VedaDbContext>();
        _customerRepository = TestWebApplicationFactory.ServiceProvider.GetRequiredService<ICustomerRepository>();
        _recipientRepository = TestWebApplicationFactory.ServiceProvider.GetRequiredService<IRecipientRepository>();
    }

    [Fact]
    public async Task DecryptEncryptedTextReturnsTheOriginalTextAsync()
    {
        // Arrange
        var recipient = await CreateCustomerAndRecipientAsync();

        // Act
        await AddDigitalContentAsync(recipient.Id, "LoremIpsum", "/Users/usavas/projects/_deployed/Veda/testdigitalcontent.txt");
        var result = await AccessDigitalContentAsync(recipient.Id, "LoremIpsum");
        
        // Check that the recipient and content were successfully created and retrieved
        Assert.NotNull(recipient);
        Assert.Equal("Lorem ipsum", recipient.FirstName);
        Assert.Single(recipient.Folder.DigitalContents);
        
        // Check the digital content properties
        var digitalContent = recipient.GetContent("LoremIpsum");
        Assert.NotNull(digitalContent);
        Assert.Equal("LoremIpsum", digitalContent.Name);
        Assert.Equal("txt", digitalContent.FileExtension);

        // Check that the file extension and memory stream are correct
        Assert.Equal("txt", result.FileExtension);
        Assert.NotNull(result.MemoryStream);

        // Verify the content of the decrypted file (if known)
        using var reader = new StreamReader(result.MemoryStream);
        result.MemoryStream.Position = 0; // Reset position to read from start
        var decryptedContent = await reader.ReadToEndAsync();
        
        // Replace "ExpectedContentHere" with the actual expected content if available
        Assert.Contains("test", decryptedContent);

        _testOutputHelper.WriteLine($"Decrypted content file extension: {result.FileExtension}");
        _testOutputHelper.WriteLine($"Decrypted content: {decryptedContent}");
    }

    private async Task<Recipient> CreateCustomerAndRecipientAsync()
    {
        var newCustomer = Customer.Create("Ali", "Lorem ipsum", DateOnly.MaxValue,
            new TCKimlikNo("12345678911"), new EmailAddress("test@test.com"), Password.Create("1234"));
        newCustomer.Memberships.Add(Membership.Create(MembershipStatus.Create("Test", 1_000_000, 10)));

        _customerRepository.Create(newCustomer);
        await _dbContext.SaveChangesAsync();

        var customer = _customerRepository.GetAllIncludingAll()
            .FirstOrDefault(c => c.FirstName.Equals("Ali")) ?? throw new InvalidOperationException("Customer creation failed");

        var newRecipient = Recipient.Create(customer.Id, "Lorem ipsum", "Dolor",
            "12345678911", "test@test.com", "+90", 12345677, DateOnly.MinValue);

        _recipientRepository.Create(newRecipient);
        await _dbContext.SaveChangesAsync();

        return _recipientRepository.GetByIdIncludingAllDigitalContent(newRecipient.Id)
            ?? throw new InvalidOperationException("Recipient creation failed");
    }

    private async Task AddDigitalContentAsync(int recipientId, string contentName, string filePath)
    {
        await Sender.Send(new AddDigitalContentCommand(recipientId, contentName, File.OpenRead(filePath), "txt"));
    }

    private async Task<AccessDigitalContentResult> AccessDigitalContentAsync(int recipientId, string contentName)
    {
        var recipient = _recipientRepository.GetByIdIncludingAllDigitalContent(recipientId)
            ?? throw new InvalidOperationException("Recipient retrieval failed");

        var contentId = recipient.GetContent(contentName)?.Id 
            ?? throw new InvalidOperationException($"Content '{contentName}' not found for recipient");

        return await Sender.Send(new AccessDigitalContentCommand(recipientId, contentId));
    }
}