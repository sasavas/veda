using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Application.UseCases.CustomerUseCases;

namespace Veda.Api.Controllers;

public class CustomerController(ISender mediatr) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<RegisterCustomerResult>> RegisterCustomer(RegisterCustomerCommand registerCustomerCommand)
    {
        return Ok(await mediatr.Send(registerCustomerCommand));
    }
}