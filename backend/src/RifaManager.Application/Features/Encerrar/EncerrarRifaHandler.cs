using Ardalis.Result;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Application.Abstractions.Results;
using RifaManager.Domain.Abstractions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.Features.Encerrar;

public sealed class EncerrarRifaHandler(IRifaRepository rifaRepository, IUnitOfWork unitOfWork) : IEncerrarRifaUseCase
{
    public async Task<Result<EncerrarRifaResponse>> Execute(Guid id)
    {
        if (id == Guid.Empty)
            return EntityErrors.EntityIdInvalid.ToResult<EncerrarRifaResponse>();

        Rifa? rifa = await rifaRepository.GetByIdForUpdateAsync(id);

        if (rifa is null)
            return RifaErrors.NaoEncontrada.ToResult<EncerrarRifaResponse>();

        try
        {
            rifa.Encerrar();

            await unitOfWork.SaveChangesAsync();

            return Result<EncerrarRifaResponse>.Success(new EncerrarRifaResponse(rifa.Id, rifa.Encerrada));
        }
        catch (DomainException exception)
        {
            return exception.Error.ToResult<EncerrarRifaResponse>();
        }
    }
}
