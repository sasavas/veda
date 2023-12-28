using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.UseCases.RecipientUseCases;

namespace Veda.Api.Controllers;

public class RecipientController(ISender mediator) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Recipient>>> AddRecipient(AddRecipientCommand command)
    {
        var recipient = await mediator.Send(command);
        return Ok(recipient);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRecipient(int id)
    {
        await mediator.Send(new DeleteRecipientCommand(id));
        return Ok();
    }
}