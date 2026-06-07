using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Rifas.GetById;

public sealed class GetRifaByIdHandler(IRifaRepository rifaRepository) : IGetRifaByIdUseCase
{
    public async Task<GetRifaByIdResponse> Execute(Guid id, CancellationToken cancellationToken)
    {
        Rifa rifa = await rifaRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Rifa nao encontrada.");

        return new GetRifaByIdResponse
        (
            rifa.Id,
            rifa.Nome,
            rifa.Descricao,
            rifa.ValorBilhete,
            rifa.DataSorteio,
            rifa.Premio,
            rifa.Encerrada
        );
    }
}
