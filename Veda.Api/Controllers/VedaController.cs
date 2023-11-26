using Microsoft.AspNetCore.Mvc;

namespace Veda.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VedaController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Test");
    }
}