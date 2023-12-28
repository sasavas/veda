using MediatR;
using Microsoft.Extensions.Logging;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.RecipientUseCases;

public record RemoveRecipientCommand(int RecipientId) : IRequest;

public class RemoveRecipientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveRecipientCommand>
{
    public Task Handle(RemoveRecipientCommand request, CancellationToken cancellationToken)
    {
        // var customerRepository = unitOfWork.GetRepository<Customer>();
        // var recipientRepository = unitOfWork.GetRepository<Recipient>();
        //
        // var recipient = recipientRepository.GetById(request.RecipientId);
        // var customer = customerRepository.GetById(recipient.) 
        //                ?? throw new NotFoundException(nameof(Customer));
        //
        // unitOfWork.BeginTransaction();
        // try
        // {
        //     var createdRecipient = recipientRepository.Create(recipient);
        //     customer.RecipientIds.Add(createdRecipient.Id);
        //
        //     unitOfWork.Commit();
        //
        //     return Task.FromResult(recipient);
        // }
        // catch (Exception e)
        // {
        //     unitOfWork.Rollback();
        //     logger.Log(LogLevel.Error, e, "Error while creating Recipient");
        //     Console.WriteLine(e);
        //     throw;
        // }
        // finally
        // {
        //     unitOfWork.Dispose();
        // }

        return Task.CompletedTask;
    }
}