using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Api.DTOs.Requests;
using Veda.Api.DTOs.Responses;
using Veda.Application.UseCases.CustomerUseCases;
using Veda.Application.UseCases.VaultUseCases;

namespace Veda.Api.Controllers;

public class AdminController(ISender mediator): BaseController
{
    [HttpPost("ChangeCustomerMembership")]
    public async Task<ActionResult> ChangeCustomerMembership(ChangeCustomerMembershipDto changeCustomerMembershipDto)
    {
        await mediator.Send(
            new StartCustomerSubscriptionCommand(changeCustomerMembershipDto.CustomerId, 
                                                        changeCustomerMembershipDto.MembershipStatusId));

        return Ok();
    }
    
    [HttpPost("ListCustomerFiles/{customerId:int}")]
    public async Task<ActionResult<IEnumerable<AdminVaultListingFileDto>>> ListCustomerFiles(int customerId)
    {
        var files = await mediator.Send(new GetCustomerFilesRequest(customerId));

        var summaryFiles = files
            .Select(file => new AdminVaultListingFileDto(
                                                            file.FileExtension,
                                                            file.Name[..2],
                                                            file.SizeInBytes,
                                                            file.UploadDate));
        
        return Ok(summaryFiles);
    }
    
    [HttpPost("ListRecipientFiles/{customerId:int}")]
    public async Task<ActionResult<IEnumerable<AdminVaultListingFileDto>>> ListRecipientFiles(int customerId)
    {
        var files = await mediator.Send(new GetRecipientFilesRequest(customerId));
        
        var summaryFiles = files
            .Select(file => new AdminVaultListingFileDto(
                                                            file.FileExtension,
                                                            //TODO show some part of the file name to the admin/operator
                                                            file.Name?[..2] ?? string.Empty,
                                                            file.SizeInBytes,
                                                            file.UploadDate));
        return Ok(summaryFiles);
    }

    [HttpDelete("ClearCustomerContent/{customerId:int}")]
    public async Task<ActionResult> ClearCustomerContent(int customerId)
    {
        await mediator.Send(new ClearCustomerContentsCommand(customerId));
        return Ok();
    }
    
    [HttpDelete("DeleteSingleContent/{recipientId:int}/{digitalContentId:int}")]
    public async Task<ActionResult> DeleteSingleContent(int recipientId, int digitalContentId)
    {
        await mediator.Send(new DeleteDigitalContentCommand(recipientId, digitalContentId));
        return Ok();
    }
}