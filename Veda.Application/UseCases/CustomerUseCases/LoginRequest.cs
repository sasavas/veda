using MediatR;
using Veda.Application.DatabaseAccess;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports;
using Veda.Application.SharedKernel.Exceptions;
using Veda.Application.SharedKernel.Models;

namespace Veda.Application.UseCases.CustomerUseCases;

public record LoginRequest(string Email, string Password) : IRequest<Customer>;

public class LoginRequestHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IRequestHandler<LoginRequest, Customer>
{
    public Task<Customer> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var customerRepository = unitOfWork.GetRepository<Customer>();

        var customer = customerRepository.GetUnique(user => user.EmailAddress == new EmailAddress(request.Email));
        if (customer == null)
        {
            throw new NotFoundException(nameof(customer));
        }

        if (!passwordHasher.VerifyPassword(request.Password, customer.Password.Value))
        {
            throw new ApplicationException("Password is wrong");
        }

        return Task.FromResult(customer);
    }
}