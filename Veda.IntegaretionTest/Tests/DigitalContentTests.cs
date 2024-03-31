using System.Text;
using Veda.Application.SharedKernel.Models;
using Veda.Application.UseCases.CustomerUseCases;
using Veda.Application.UseCases.RecipientUseCases;
using Veda.Application.UseCases.VaultUseCases;
using Veda.IntegaretionTest.Abstract;

namespace Veda.IntegaretionTest.Tests;

public class DigitalContentTests : BaseIntegrationTest
{
    public DigitalContentTests(TestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddingDigitalContent_HappyPath()
    {
        var customer = await Sender.Send(
            new RegisterCustomerCommand(
                "Test", "Test", "12345678912", "test@tets.com", "1234",
                new DateOnly(2001, 12, 12),
                new PhoneNumber("+90", 5446669900)));

        var membershipStatuses = await Sender.Send(new GetMembershipStatusesRequest());

        await Sender.Send(new StartCustomerSubscriptionCommand(customer.Customer.Id, membershipStatuses.First().Id));

        var recipient = await Sender.Send(
            new AddRecipientCommand(
                customer.Customer.Id, "Test", "Test", "12345678914",
                "test@test.com", "+90", 5446668877, 
                new DateOnly(2000, 1, 1)));

        await Sender.Send(new AddDigitalContentCommand(recipient.Id, "testfile", GenerateRandomFile("test")));
    }

    private static MemoryStream GenerateRandomFile(string text)
    {
        MemoryStream memoryStream = new MemoryStream();
        byte[] bytes = Encoding.UTF8.GetBytes(text);

        memoryStream.Write(bytes, 0, bytes.Length);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}