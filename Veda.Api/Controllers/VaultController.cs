using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.UseCases.VaultUseCases;

namespace Veda.Api.Controllers;

public class VaultController(IMediator mediator) : BaseController
{
    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<IEnumerable<Folder>>> GetAllCustomerContent(int customerId)
    {
        var folders = await mediator.Send(new GetCustomerFilesRequest(customerId));
        return Ok(folders);
    }
}