using Ardalis.Result;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Application.Abstractions.Results;
using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.Features.Atualizar;

public sealed class AtualizarRifaHandler(IRifaRepository rifaRepository, IUnitOfWork unitOfWork) : IAtualizarRifaUseCase
{
    public async Task<Result<AtualizarRifaResponse>> Execute(Guid id, AtualizarRifaRequest request)
    {
        if (id == Guid.Empty)
            return EntityErrors.EntityIdInvalid.ToResult<AtualizarRifaResponse>();

        if (request is null)
            return RifaErrors.RequestObrigatorio.ToResult<AtualizarRifaResponse>();

        Rifa? rifa = await rifaRepository.GetByIdForUpdateAsync(id);

        if (rifa is null)
            return RifaErrors.NaoEncontrada.ToResult<AtualizarRifaResponse>();

        try
        {
            rifa.Atualizar(request.Nome, request.Descricao, request.ValorBilhete, request.DataSorteio, request.Premio);

            await unitOfWork.SaveChangesAsync();

            return Result<AtualizarRifaResponse>.Success(new AtualizarRifaResponse(rifa.Id, rifa.Nome, rifa.ValorBilhete, rifa.DataSorteio, rifa.Premio));
        }
        catch (DomainException exception)
        {
            return exception.Error.ToResult<AtualizarRifaResponse>();
        }
    }
}
