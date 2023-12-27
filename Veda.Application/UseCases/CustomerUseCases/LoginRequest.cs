using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.UseCases.CustomerUseCases;

public record LoginRequest(string Email, string Password) : IRequest<Customer>;

public class LoginRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginRequest, Customer>
{
    public Task<Customer> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var customerRepository = unitOfWork.GetRepository<Customer>();
        
        //TODO encrypt password
        var customer =
            customerRepository.GetUnique(user =>
                user.EmailAddress == new EmailAddress(request.Email) && user.Password == new Password(request.Password));

        if (customer == null)
        {
            throw new ApplicationException("Cannot login with the given user credentials");
        }

        return Task.FromResult(customer);
    }
}