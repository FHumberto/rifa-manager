using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Login;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
public sealed class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    [EndpointSummary("Login")]
    [EndpointDescription("Realiza o login do usuário e retorna um token de autenticação.")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginResponse result = await useCase.Execute(request, cancellationToken);
        return Ok(result);
    }
}
