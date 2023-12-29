using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports;
using Veda.Application.SharedKernel.Models;
using Veda.SharedKernel.Services.Email;
using Veda.SharedKernel.Services.HtmlHelper;
using EmailAddress = Veda.Application.SharedKernel.Models.EmailAddress;

namespace Veda.Application.UseCases.CustomerUseCases;

public record struct RegisterCustomerCommand(
    string FirstName,
    string LastName,
    string TcKimlikNo,
    string EmailAddress,
    string Password,
    DateOnly DateOfBirth,
    PhoneNumber? PhoneNumber
    ) : IRequest<RegisterCustomerResult>;

public record RegisterCustomerResult(Customer Customer);

public class RegisterCustomerCommandHandler(
    ILogger<RegisterCustomerCommand> _logger,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IEmailService emailService,
    IHtmlService htmlService) : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResult>
{
    public Task<RegisterCustomerResult> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var customer = Customer.Create(
                command.FirstName,
                command.LastName,
                command.DateOfBirth,
                new TCKimlikNo(command.TcKimlikNo),
                new EmailAddress(command.EmailAddress),
                Password.Create(passwordHasher.HashPassword(command.Password)));

            if (command.PhoneNumber != null)
            {
                customer.PhoneNumber = command.PhoneNumber;
            }

            //TODO send via Domain events
            emailService.SendEmail(
                new HtmlEmailDTO(
                    new Veda.SharedKernel.Services.Email.EmailAddress(customer.EmailAddress.Value),
                    "Please verify your email address",
                    htmlService
                        .GetHtmlBuilder()
                        .AddTitle("Welcome to Veda!")
                        .AddParagraph("Please verify your membership by clicking the following link")
                        .AddParagraph("If you were not expecting this email, please ignore")
                        .AddLink("www.google.com") //TODO: link to provide user to go to and verify registration
                        .Build()));

            unitOfWork.BeginTransaction();

            var registeredCustomer = unitOfWork.GetRepository<Customer>().Create(customer);
            unitOfWork.Commit();

            return Task.FromResult(new RegisterCustomerResult(registeredCustomer));
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            _logger.Log(LogLevel.Error, e, "Customer could not be created");
            throw;
        }
    }
}