using Ardalis.Result;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Application.Abstractions.Results;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.Features.GetById;

public sealed class GetRifaByIdHandler(IRifaRepository rifaRepository) : IGetRifaByIdUseCase
{
    public async Task<Result<GetRifaByIdResponse>> Execute(Guid id)
    {
        if (id == Guid.Empty)
            return EntityErrors.EntityIdInvalid.ToResult<GetRifaByIdResponse>();

        Rifa? rifa = await rifaRepository.GetByIdAsync(id);

        return rifa is null
            ? RifaErrors.NaoEncontrada.ToResult<GetRifaByIdResponse>()
            : Result<GetRifaByIdResponse>.Success(new GetRifaByIdResponse(rifa.Nome, rifa.ValorBilhete, rifa.Bilhetes.Count, rifa.DataSorteio));
    }
}
