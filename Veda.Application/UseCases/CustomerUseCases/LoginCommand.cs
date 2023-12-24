using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Exceptions;

namespace Veda.Application.UseCases.CustomerUseCases;

public record LoginCommand(string Email, string Password) : IRequest;

public class LoginCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<LoginCommand>
{
    public Task Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //TODO encrypt password
        var result = customerRepository.GetUnique(user =>
                         user.EmailAddress.Equals(request.Email) && user.Password.Equals(request.Password)) 
                     ?? throw new NotFoundException<Customer>();

        //TODO generate and return JWT Token
        
        return Task.CompletedTask;
    }
}