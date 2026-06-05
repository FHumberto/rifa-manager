using Ardalis.Result;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RifaManager.Application.Features.Atualizar;
using RifaManager.Application.Features.Cadastrar;
using RifaManager.Application.Features.Encerrar;
using RifaManager.Application.Features.GetById;
using RifaManager.Application.Features.Listar;

namespace RifaManager.Api.Controllers.v1;

[ApiVersion(1)]
public sealed class RifasController : BaseController
{
    #region [ LEITURA ]

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ListarRifasResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarRifas([FromServices] IListarRifasUseCase useCase)
    {
        Result<IReadOnlyList<ListarRifasResponse>> result = await useCase.Execute();

        return result.IsSuccess ? Ok(result.Value) : Problem(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetRifaByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRifaById([FromServices] IGetRifaByIdUseCase useCase, Guid id)
    {
        Result<GetRifaByIdResponse> result = await useCase.Execute(id);

        return result.IsSuccess ? Ok(result.Value) : Problem(result);
    }

    #endregion

    #region [ ESCRITA ]

    [HttpPost]
    [ProducesResponseType(typeof(CadastrarRifaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarRifa([FromServices] ICadastrarRifaUseCase useCase, [FromBody] CadastrarRifaRequest request)
    {
        Result<CadastrarRifaResponse> result = await useCase.Execute(request);

        return result.IsSuccess ? CreatedAtAction(nameof(GetRifaById), new { id = result.Value.Id }, result.Value)
                                : Problem(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AtualizarRifaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarRifa([FromServices] IAtualizarRifaUseCase useCase, Guid id, [FromBody] AtualizarRifaRequest request)
    {
        Result<AtualizarRifaResponse> result = await useCase.Execute(id, request);

        return result.IsSuccess ? Ok(result.Value) : Problem(result);
    }

    [HttpPatch("{id:guid}/encerrar")]
    [ProducesResponseType(typeof(EncerrarRifaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> EncerrarRifa([FromServices] IEncerrarRifaUseCase useCase, Guid id)
    {
        Result<EncerrarRifaResponse> result = await useCase.Execute(id);

        return result.IsSuccess ? Ok(result.Value) : Problem(result);
    }

    #endregion
}
