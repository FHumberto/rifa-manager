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

[Authorize]
[ApiVersion(1)]
public sealed class BilhetesController : BaseController
{
    #region [ LEITURA ]

    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter bilhete por id")]
    [ProducesResponseType(typeof(GetBilheteByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetBilheteByIdUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetBilheteByIdResponse response = await useCase.Execute(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet("rifa/{rifaId:guid}")]
    [EndpointSummary("Listar bilhetes por rifa")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarBilhetesPorRifaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorRifa([FromServices] IListarBilhetesPorRifaUseCase useCase, [FromRoute] Guid rifaId, CancellationToken cancellationToken)
    {
        IReadOnlyList<ListarBilhetesPorRifaResponse> response = await useCase.Execute(rifaId, cancellationToken);

        return Ok(response);
    }

    [HttpGet("status/{status}")]
    [EndpointSummary("Listar bilhetes por status")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarBilhetesPorStatusResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorStatus([FromServices] IListarBilhetesPorStatusUseCase useCase, [FromRoute] StatusPagamento status, [FromQuery] Guid? rifaId, CancellationToken cancellationToken)
    {
        IReadOnlyList<ListarBilhetesPorStatusResponse> response = await useCase.Execute(status, rifaId, cancellationToken);

        return Ok(response);
    }

    #endregion

    #region [ ESCRITA ]

    [HttpPost("compras")]
    [EndpointSummary("Registrar compra de bilhetes")]
    [ProducesResponseType(typeof(RegistrarCompraBilhetesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RegistrarCompra([FromServices] IRegistrarCompraBilhetesUseCase useCase, [FromBody] RegistrarCompraBilhetesRequest request, CancellationToken cancellationToken)
    {
        Guid usuarioResponsavelId = GetUsuarioAutenticadoId();
        RegistrarCompraBilhetesResponse response = await useCase.Execute(usuarioResponsavelId, request, cancellationToken);

        return Created(string.Empty, response);
    }

    [HttpPatch("{id:guid}/status")]
    [EndpointSummary("Alterar status do bilhete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AlterarStatus([FromServices] IAlterarStatusBilheteUseCase useCase, [FromRoute] Guid id, [FromBody] AlterarStatusBilheteRequest request, CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/cancelar")]
    [EndpointSummary("Cancelar bilhete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancelar([FromServices] ICancelarBilheteUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await useCase.Execute(id, cancellationToken);

        return NoContent();
    }

    #endregion
}
