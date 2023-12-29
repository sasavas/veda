using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Api.DTOs;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.UseCases.VaultUseCases;

namespace Veda.Api.Controllers;

public class VaultController(ISender mediator) : BaseController
{
    [HttpGet("Customer/{customerId:int}")]
    public async Task<ActionResult<IEnumerable<Folder>>> GetAllCustomerContents(int customerId)
    {
        var contents = await mediator.Send(new GetCustomerFilesRequest(customerId));
        return Ok(contents);
    }
    
    [HttpGet("Recipient/{recipientId:int}")]
    public async Task<ActionResult<IEnumerable<Folder>>> GetAllRecipientContents(int recipientId)
    {
        var contents = await mediator.Send(new GetRecipientFilesRequest(recipientId));
        return Ok(contents);
    }

    [HttpPost]
    public async Task<ActionResult> AddDigitalContent([FromForm] AddDigitalContentDto digitalContentDto)
    {
        //TODO: send the acutal file after converting to stream
        // await using FileStream fileStream = new FileStream();
        // await digitalContentDto.file.CopyToAsync(fileStream);
        
        await mediator.Send(new AddDigitalContentCommand(
            digitalContentDto.recipientId, digitalContentDto.fileName, null));
        return Ok();
    }

    [HttpDelete("{recipientId:int}/{digitalContentId:int}")]
    public async Task<ActionResult> DeleteDigitalContent(int recipientId, int digitalContentId)
    {
        await mediator.Send(new DeleteDigitalContentCommand(recipientId, digitalContentId));
        return Ok();
    }
}