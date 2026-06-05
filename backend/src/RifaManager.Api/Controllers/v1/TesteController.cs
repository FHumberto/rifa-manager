using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion("1")]
[Tags("Teste")]
public class TesteController : BaseController
{
    [HttpGet]
    [EndpointSummary("Teste")]
    [EndpointDescription("Se está na V1.")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Check() => Ok("V1 OK!");
}
