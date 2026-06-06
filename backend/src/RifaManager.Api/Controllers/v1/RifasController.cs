using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Rifas.CadastrarRifa;
using RifaManager.Application.UseCases.Rifas.EditarRifa;
using RifaManager.Application.UseCases.Rifas.EncerrarRifa;
using RifaManager.Application.UseCases.Rifas.GetById;
using RifaManager.Application.UseCases.Rifas.ListarRifas;
using RifaManager.Application.UseCases.Rifas.SortearRifa;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
[Authorize]
public sealed class RifasController : BaseController
{
    [HttpGet]
    [EndpointSummary("Listar rifas")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarRifasResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Listar([FromServices] IListarRifasUseCase useCase)
    {
        IReadOnlyList<ListarRifasResponse> response = await useCase.Execute();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter rifa por id")]
    [ProducesResponseType(typeof(GetRifaByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetRifaByIdUseCase useCase, [FromRoute] Guid id)
    {
        GetRifaByIdResponse response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpPost]
    [EndpointSummary("Cadastrar rifa")]
    [ProducesResponseType(typeof(CadastrarRifaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Cadastrar([FromServices] ICadastrarRifaUseCase useCase, [FromBody] CadastrarRifaRequest request)
    {
        CadastrarRifaResponse response = await useCase.Execute(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [EndpointSummary("Editar rifa")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar([FromServices] IEditarRifaUseCase useCase, [FromRoute] Guid id, [FromBody] EditarRifaRequest request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpPatch("{id:guid}/encerrar")]
    [EndpointSummary("Encerrar rifa")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Encerrar([FromServices] IEncerrarRifaUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPost("{id:guid}/sortear")]
    [EndpointSummary("Sortear rifa")]
    [ProducesResponseType(typeof(SortearRifaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Sortear([FromServices] ISortearRifaUseCase useCase, [FromRoute] Guid id)
    {
        SortearRifaResponse response = await useCase.Execute(id);

        return Ok(response);
    }
}
