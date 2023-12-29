using Microsoft.AspNetCore.Mvc;
using Veda.Api.Abstract;

namespace Veda.Api.Controllers;

public class FinancialsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult> GetFatura()
    {
        return Ok("Fatura bilgileri");
    }
}