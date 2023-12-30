using MediatR;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Api.DTOs.Requests;
using Veda.Api.DTOs.Responses;
using Veda.Application.UseCases.AdminUseCases;
using Veda.Application.UseCases.CustomerUseCases;
using Veda.Application.UseCases.VaultUseCases;

namespace Veda.Api.Controllers;

public class AdminController(ISender mediator): BaseController
{
    [HttpGet("ListAllCustomers")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> ListAllCustomers()
    {
        var customers = await mediator.Send(new ListAllCustomersRequest());
        var summaryCustomers = customers.Select(CustomerDto.FromCustomerEntity);
        
        return Ok(summaryCustomers);
    }
    
    [HttpGet("ListActiveCustomers")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> ListActiveCustomers()
    {
        var customers = await mediator.Send(new ListAllCustomersRequest());
        var summaryCustomers = customers
            .Where(customer => customer.ActiveMembership is not null)
            .Select(CustomerDto.FromCustomerEntity);
        
        return Ok(summaryCustomers);
    }
    
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
        var summaryFiles = files.Select(AdminVaultListingFileDto.FromDigitalContent);
        return Ok(summaryFiles);
    }
    
    [HttpPost("ListRecipientFiles/{customerId:int}")]
    public async Task<ActionResult<IEnumerable<AdminVaultListingFileDto>>> ListRecipientFiles(int customerId)
    {
        var files = await mediator.Send(new GetRecipientFilesRequest(customerId));
        var summaryFiles = files.Select(AdminVaultListingFileDto.FromDigitalContent);
        return Ok(summaryFiles);
    }
    
    [HttpDelete("DeactivateCustomerAccount/{customerId:int}")]
    public async Task<ActionResult> DeactivateCustomer(int customerId)
    {
        await mediator.Send(new DeactivateCustomerCommand(customerId));
        return Ok();
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