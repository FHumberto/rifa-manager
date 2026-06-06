using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.AtivarUsuario;
using RifaManager.Application.UseCases.DesativarUsuario;
using RifaManager.Application.UseCases.EditarUsuario;
using RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetUsuarioByIdUseCase useCase, [FromRoute] Guid id)
    {
        GetUsuarioByIdResponse response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPost]
    [EndpointSummary("Cadastrar usuario")]
    [ProducesResponseType(typeof(CadastrarUsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Cadastrar([FromServices] ICadastrarUsuarioUseCase useCase, [FromBody] CadastrarUsuarioRequest request)
    {
        CadastrarUsuarioResponse response = await useCase.Execute(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [EndpointSummary("Editar usuario")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar([FromServices] IEditarUsuarioUseCase useCase, [FromRoute] Guid id, [FromBody] EditarUsuarioRequest request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPatch("{id:guid}/ativar")]
    [EndpointSummary("Ativar usuario")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Ativar([FromServices] IAtivarUsuarioUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPatch("{id:guid}/desativar")]
    [EndpointSummary("Desativar usuario")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar([FromServices] IDesativarUsuarioUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
