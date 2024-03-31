using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.Modules.CustomerModule.Exceptions;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.Ports.DataAccess;
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

public class AddRecipientCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<AddRecipientCommandHandler> logger)
    : IRequestHandler<AddRecipientCommand, Recipient>
{
    public Task<Recipient> Handle(AddRecipientCommand request, CancellationToken cancellationToken)
    {
        var customer = unitOfWork.GetRepository<Customer>().GetById(request.CustomerId) 
                ?? throw new NotFoundException(nameof(Customer));

        if (customer.IsMaxRecipientCapacityReached())
        {
            throw new CustomerHasReachedRecipientLimit(
                "Customer cannot have more Recipients within the limit of current membership");
        }
        
        var recipient = Recipient.Create(
            request.CustomerId, request.FirstName, request.LastName, request.TcKimlikNo, request.EmailAddress,
            request.PhoneNumberCountryCode, request.PhoneNumber, request.DateOfBirth);
        
        unitOfWork.BeginTransaction();
        try
        {
            var createdRecipient = unitOfWork.GetRepository<Recipient>().Create(recipient);

            unitOfWork.Commit();

            return Task.FromResult(createdRecipient);
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