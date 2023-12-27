using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Veda.Api.Abstract;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected string? GetLoginCustomerTcKimlikNo()
    {
        if (User.Identity is not ClaimsIdentity claimsIdentity) return null;
        
        var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        return userIdClaim?.Value;
    }
}