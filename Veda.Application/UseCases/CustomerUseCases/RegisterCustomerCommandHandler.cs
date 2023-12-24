using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;
using Veda.Application.SharedKernel.Services.Email;
using Veda.Application.SharedKernel.Services.HtmlHelper;

namespace Veda.Application.UseCases.CustomerUseCases;

public record struct RegisterCustomerCommand(
    string FirstName, string LastName, DateOnly DateOfBirth,
    string TcKimlikNo, string EmailAddress, string Password): IRequest<RegisterCustomerResult>;

public record RegisterCustomerResult(Customer Customer);

public class RegisterCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IEmailService emailService,
    IHtmlService htmlService) : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResult>
{
    public Task<RegisterCustomerResult> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            TCKimlikNo = new TCKimlikNo(command.TcKimlikNo),
            DateOfBirth = command.DateOfBirth,
            EmailAddress = new EmailAddress(command.EmailAddress),
            Password = new Password(command.Password)
        };

        //TODO send via Domain events
        emailService.SendEmail(
            new HtmlEmailDTO(
                customer.EmailAddress,
                "Please verify your email address",
                htmlService
                    .GetHtmlBuilder()
                    .AddTitle("Welcome to Veda!")
                    .AddParagraph("Please verify your membership by clicking the link following link")
                    .AddLink("")
                    .Build()));

        var registeredCustomer = customerRepository.Create(customer);
        return Task.FromResult(new RegisterCustomerResult(registeredCustomer));
    }
}