using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Usuarios.GetById;
using RifaManager.Domain.Enums;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
[Authorize(Roles = nameof(PerfilUsuario.Administrador))]
public sealed class UsuarioController : BaseController
{
    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter usuario por id")]
    [ProducesResponseType(typeof(GetUsuarioByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetUsuarioByIdUseCase useCase, [FromRoute] Guid id)
    {
        GetUsuarioByIdResponse response = await useCase.Execute(id);

        return Ok(response);
    }
}
