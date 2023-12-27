using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Api.Helpers;
using Veda.Application.UseCases.CustomerUseCases;

namespace Veda.Api.Controllers;

public class CustomerController(ISender mediatr, JwtProvider jwtProvider) : BaseController
{
    [HttpPost("Register")]
    public async Task<ActionResult<RegisterCustomerResult>> RegisterCustomer(RegisterCustomerCommand registerCustomerCommand)
    {
        return Ok(await mediatr.Send(registerCustomerCommand));
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginCommand loginCommand)
    {
        // retrieve customer
        var customer = await mediatr.Send(loginCommand);

        // generate token
        var token = jwtProvider.Generate(customer);
        return token;
    }
}