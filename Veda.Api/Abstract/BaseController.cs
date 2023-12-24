using Microsoft.AspNetCore.Mvc;

namespace Veda.Api.Abstract;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
}