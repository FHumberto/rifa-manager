using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Rifas.CadastrarRifa;
using RifaManager.Application.UseCases.Rifas.EditarRifa;
using RifaManager.Application.UseCases.Rifas.EncerrarRifa;
using RifaManager.Application.UseCases.Rifas.GetById;
using RifaManager.Application.UseCases.Rifas.ListarRifas;
using RifaManager.Application.UseCases.Rifas.SortearRifa;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
public sealed class RifasController : BaseController
{
    #region [ LEITURA ]

    [HttpGet]
    [EndpointSummary("Listar rifas")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarRifasResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Listar([FromServices] IListarRifasUseCase useCase, CancellationToken cancellationToken)
    {
        IReadOnlyList<ListarRifasResponse> response = await useCase.Execute(cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter rifa por id")]
    [ProducesResponseType(typeof(GetRifaByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetRifaByIdUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetRifaByIdResponse response = await useCase.Execute(id, cancellationToken);

        return Ok(response);
    }

    #endregion

    #region [ ESCRITA ]

    [HttpPost]
    [EndpointSummary("Cadastrar rifa")]
    [ProducesResponseType(typeof(CadastrarRifaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Cadastrar([FromServices] ICadastrarRifaUseCase useCase, [FromBody] CadastrarRifaRequest request, CancellationToken cancellationToken)
    {
        CadastrarRifaResponse response = await useCase.Execute(request, cancellationToken);

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [EndpointSummary("Editar rifa")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar([FromServices] IEditarRifaUseCase useCase, [FromRoute] Guid id, [FromBody] EditarRifaRequest request, CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/encerrar")]
    [EndpointSummary("Encerrar rifa")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Encerrar([FromServices] IEncerrarRifaUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await useCase.Execute(id, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/sortear")]
    [EndpointSummary("Sortear rifa")]
    [ProducesResponseType(typeof(SortearRifaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Sortear([FromServices] ISortearRifaUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        SortearRifaResponse response = await useCase.Execute(id, cancellationToken);

        return Ok(response);
    }
    #endregion
}
