using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.UseCases.VaultUseCases;

namespace Veda.Api.Controllers;

public class VaultController(ISender mediator) : BaseController
{
    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<IEnumerable<Folder>>> GetAllCustomerContent(int customerId)
    {
        var folders = await mediator.Send(new GetCustomerFilesRequest(customerId));
        return Ok(folders);
    }

    [HttpPost]
    public async Task<ActionResult> AddDigitalContent(int recipientId, string fileName, IFormFile file)
    {
        //TODO: send the acutal file after converting to stream
        await mediator.Send(new AddDigitalContentCommand(recipientId, fileName, null));
        return Ok();
    }
}