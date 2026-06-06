using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;
using RifaManager.Application.UseCases.Bilhetes.CancelarBilhete;
using RifaManager.Application.UseCases.Bilhetes.GetById;
using RifaManager.Application.UseCases.Bilhetes.ListarPorRifa;
using RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;
using RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;
using RifaManager.Domain.Enums;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
[Authorize]
public sealed class BilhetesController : BaseController
{
    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter bilhete por id")]
    [ProducesResponseType(typeof(GetBilheteByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetBilheteByIdUseCase useCase, [FromRoute] Guid id)
    {
        GetBilheteByIdResponse response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("rifa/{rifaId:guid}")]
    [EndpointSummary("Listar bilhetes por rifa")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarBilhetesPorRifaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorRifa([FromServices] IListarBilhetesPorRifaUseCase useCase, [FromRoute] Guid rifaId)
    {
        IReadOnlyList<ListarBilhetesPorRifaResponse> response = await useCase.Execute(rifaId);

        return Ok(response);
    }

    [HttpGet("status/{status}")]
    [EndpointSummary("Listar bilhetes por status")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarBilhetesPorStatusResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorStatus([FromServices] IListarBilhetesPorStatusUseCase useCase, [FromRoute] StatusPagamento status, [FromQuery] Guid? rifaId)
    {
        IReadOnlyList<ListarBilhetesPorStatusResponse> response = await useCase.Execute(status, rifaId);

        return Ok(response);
    }

    [HttpPost("compras")]
    [EndpointSummary("Registrar compra de bilhetes")]
    [ProducesResponseType(typeof(RegistrarCompraBilhetesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarCompra([FromServices] IRegistrarCompraBilhetesUseCase useCase, [FromBody] RegistrarCompraBilhetesRequest request)
    {
        Guid usuarioResponsavelId = GetUsuarioAutenticadoId();
        RegistrarCompraBilhetesResponse response = await useCase.Execute(usuarioResponsavelId, request);

        return Created(string.Empty, response);
    }

    [HttpPatch("{id:guid}/status")]
    [EndpointSummary("Alterar status do bilhete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AlterarStatus([FromServices] IAlterarStatusBilheteUseCase useCase, [FromRoute] Guid id, [FromBody] AlterarStatusBilheteRequest request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    [EndpointSummary("Cancelar bilhete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancelar([FromServices] ICancelarBilheteUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    private Guid GetUsuarioAutenticadoId()
    {
        string? usuarioId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.Parse(usuarioId!);
    }
}
