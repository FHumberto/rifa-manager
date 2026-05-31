using Microsoft.AspNetCore.Mvc;

namespace RifaManager.Api.Controllers;

[ApiController]
[Route("/")]
[Tags("A P I")]
public sealed class DefaultController : BaseController
{
    [HttpGet]
    [EndpointSummary("Checagem")]
    [EndpointDescription("Verifica se a API está funcional.")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Check() => Ok("API OK!");
}
