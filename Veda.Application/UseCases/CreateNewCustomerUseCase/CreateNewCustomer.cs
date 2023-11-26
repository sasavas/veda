using Veda.Application.Abstract;
using Veda.Application.DTOs;
using Veda.Application.Modules.CustomerModule.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;
using Veda.Application.SharedKernel.Services.Email;
using Veda.Application.SharedKernel.Services.HtmlHelper;

namespace Veda.Application.UseCases.CreateNewCustomerUseCase;

public class CreateNewCustomer : UseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IEmailService _emailService;
    private readonly IHtmlService _htmlService;

    public CreateNewCustomer(
        ICustomerRepository customerRepository, IMembershipRepository membershipRepository, IEmailService emailService,
        IHtmlService htmlService)
    {
        _customerRepository = customerRepository;
        _membershipRepository = membershipRepository;
        _emailService = emailService;
        _htmlService = htmlService;
    }

    public Customer Handle(CustomerCreateDTO customerCreateDto)
    {
        var customer = new Customer
        {
            FirstName = customerCreateDto.FirstName,
            LastName = customerCreateDto.LastName,
            TCKimlikNo = new TCKimlikNo(customerCreateDto.TCKimlikNo),
            DateOfBirth = customerCreateDto.DateOfBirth,
            EmailAddress = new EmailAddress(customerCreateDto.EmailAddress),
            Password = new Password(customerCreateDto.Password)
        };

        _emailService.SendEmail(
            new HtmlEmailDTO(
                customer.EmailAddress,
                "Please verify your email address",
                _htmlService
                    .GetHtmlBuilder()
                    .AddTitle("Welcome to Veda!")
                    .AddParagraph("Please verify your membership by clicking the link following link")
                    .AddLink("")
                    .Build()));

        return _customerRepository.Create(customer);
    }
}