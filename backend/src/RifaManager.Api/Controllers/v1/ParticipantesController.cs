using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.UseCases.Participantes.CadastrarParticipante;
using RifaManager.Application.UseCases.Participantes.EditarParticipante;
using RifaManager.Application.UseCases.Participantes.GetById;
using RifaManager.Application.UseCases.Participantes.ListarPorRifa;
using RifaManager.Application.UseCases.Participantes.PesquisarParticipantes;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
[Authorize]
public sealed class ParticipantesController : BaseController
{
    [HttpGet("{id:guid}")]
    [EndpointSummary("Obter participante por id")]
    [ProducesResponseType(typeof(GetParticipanteByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId([FromServices] IGetParticipanteByIdUseCase useCase, [FromRoute] Guid id)
    {
        GetParticipanteByIdResponse response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet("rifa/{rifaId:guid}")]
    [EndpointSummary("Listar participantes por rifa")]
    [ProducesResponseType(typeof(IReadOnlyList<ListarParticipantesPorRifaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListarPorRifa([FromServices] IListarParticipantesPorRifaUseCase useCase, [FromRoute] Guid rifaId)
    {
        IReadOnlyList<ListarParticipantesPorRifaResponse> response = await useCase.Execute(rifaId);

        return Ok(response);
    }

    [HttpGet("pesquisar")]
    [EndpointSummary("Pesquisar participantes")]
    [ProducesResponseType(typeof(IReadOnlyList<PesquisarParticipantesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Pesquisar([FromServices] IPesquisarParticipantesUseCase useCase, [FromQuery] PesquisarParticipantesRequest request)
    {
        IReadOnlyList<PesquisarParticipantesResponse> response = await useCase.Execute(request);

        return Ok(response);
    }

    [HttpPost]
    [EndpointSummary("Cadastrar participante")]
    [ProducesResponseType(typeof(CadastrarParticipanteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cadastrar([FromServices] ICadastrarParticipanteUseCase useCase, [FromBody] CadastrarParticipanteRequest request)
    {
        CadastrarParticipanteResponse response = await useCase.Execute(request);

        return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    [EndpointSummary("Editar participante")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar([FromServices] IEditarParticipanteUseCase useCase, [FromRoute] Guid id, [FromBody] EditarParticipanteRequest request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
