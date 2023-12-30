using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;
using Veda.Api.DTOs.Responses;
using Veda.Api.Helpers;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
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
    public async Task<ActionResult<string>> Login(LoginRequest loginRequest)
    {
        // retrieve customer
        var customer = await mediatr.Send(loginRequest);

        // generate token
        var token = jwtProvider.Generate(customer);
        return Ok(token);
    }

    [HttpGet("MembershipStatuses")]
    public async Task<ActionResult<IEnumerable<MembershipStatus>>> GetMembershipStatuses()
    {
        return Ok(await mediatr.Send(new GetMembershipStatusesRequest()));
    }

    [HttpPost("StartMembership")]
    public async Task<ActionResult> StartMembership(StartCustomerSubscriptionCommand command)
    {
        //TODO:production get login userId ([Authorize] attribute required) for security
        //// var tcKimlikNo = GetLoginCustomerTcKimlikNo();

        await mediatr.Send(command);
        return Ok();
    }

    [HttpGet("{customerId:int}/Recipients")]
    public async Task<ActionResult<IEnumerable<RecipientDto>>> GetRecipients(int customerId)
    {
        var recipients = await mediatr.Send(new GetRecipientsRequest(customerId));
        var recipientDtos = recipients
            .Select(r => new RecipientDto(r.CustomerId, 
                                                    r.FirstName, 
                                                    r.LastName, 
                                                    r.DateOfBirth, 
                                                    r.TCKimlikNo.Value, 
                                                    r.EMailAddress.Value,
                                                    r.PhoneNumber.CountryCode + r.PhoneNumber.Number, 
                                                    r.TotalCapacityOccupied()));
        
        return Ok(recipientDtos);
    }
    
    [HttpGet("AuthorizationTest/{test}")]
    [Authorize]
    public async Task<ActionResult<string>> GetCustomerInfo(string test)
    {
        return Ok(test);
    }
}