using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.RecipientUseCases;

public record AddRecipientCommand(
    int CustomerId,
    string FirstName,
    string LastName,
    string TcKimlikNo,
    string EmailAddress,
    string PhoneNumberCountryCode,
    long PhoneNumber,
    DateOnly DateOfBirth) : IRequest<Recipient>;

public class AddRecipientComandHandler(
    IUnitOfWork unitOfWork,
    ILogger<AddRecipientComandHandler> logger)
    : IRequestHandler<AddRecipientCommand, Recipient>
{
    public Task<Recipient> Handle(AddRecipientCommand request, CancellationToken cancellationToken)
    {
        var customerRepository = unitOfWork.GetRepository<Customer>();
        var recipientRepository = unitOfWork.GetRepository<Recipient>();

        var recipient = Recipient.Create(
            request.FirstName, request.LastName, request.TcKimlikNo, request.EmailAddress,
            request.PhoneNumberCountryCode, request.PhoneNumber, request.DateOfBirth);

        var customer = customerRepository.GetById(request.CustomerId) 
                       ?? throw new NotFoundException(nameof(Customer));

        unitOfWork.BeginTransaction();
        try
        {
            var createdRecipient = recipientRepository.Create(recipient);
            customer.RecipientIds.Add(createdRecipient.Id);

            unitOfWork.Commit();

            return Task.FromResult(recipient);
        }
        catch (Exception e)
        {
            unitOfWork.Rollback();
            logger.Log(LogLevel.Error, e, "Error while creating Recipient");
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            unitOfWork.Dispose();
        }
    }
}