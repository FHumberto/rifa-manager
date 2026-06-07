using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Participantes.CadastrarParticipante;
using RifaManager.Application.UseCases.Participantes.EditarParticipante;
using RifaManager.Application.UseCases.Participantes.GetById;
using RifaManager.Application.UseCases.Participantes.ListarPorRifa;
using RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

namespace RifaManager.Api.Controllers.v1;

[Authorize]
[ApiVersion(1)]
public sealed class ParticipantesController : BaseController
{
    #region [ LEITURA ]

    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter participante por id")]
    [ProducesResponseType(typeof(GetParticipanteByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetParticipanteByIdUseCase useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        GetParticipanteByIdResponse response = await useCase.Execute(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet("rifa/{rifaId:guid}")]
    [EndpointSummary("Listar participantes por rifa")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarParticipantesPorRifaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorRifa([FromServices] IListarParticipantesPorRifaUseCase useCase, [FromRoute] Guid rifaId, CancellationToken cancellationToken)
    {
        IReadOnlyList<ListarParticipantesPorRifaResponse> response = await useCase.Execute(rifaId, cancellationToken);

        return Ok(response);
    }

    [HttpGet("pesquisar")]
    [EndpointSummary("Pesquisar participantes")]
    [ProducesResponseType(typeof(IReadOnlyList<PesquisarParticipantesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Pesquisar([FromServices] IPesquisarParticipantesUseCase useCase, [FromQuery] PesquisarParticipantesRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<PesquisarParticipantesResponse> response = await useCase.Execute(request, cancellationToken);

        return Ok(response);
    }

    #endregion

    #region [ ESCRITA ]

    [HttpPost]
    [EndpointSummary("Cadastrar participante")]
    [ProducesResponseType(typeof(CadastrarParticipanteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cadastrar([FromServices] ICadastrarParticipanteUseCase useCase, [FromBody] CadastrarParticipanteRequest request, CancellationToken cancellationToken)
    {
        CadastrarParticipanteResponse response = await useCase.Execute(request, cancellationToken);

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [EndpointSummary("Editar participante")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar([FromServices] IEditarParticipanteUseCase useCase, [FromRoute] Guid id, [FromBody] EditarParticipanteRequest request, CancellationToken cancellationToken)
    {
        await useCase.Execute(id, request, cancellationToken);

        return NoContent();
    }

    #endregion
}
