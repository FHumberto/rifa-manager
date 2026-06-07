using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Rifas.ListarRifas;

public sealed class ListarRifasHandler(IRifaRepository rifaRepository) : IListarRifasUseCase
{
    public async Task<IReadOnlyList<ListarRifasResponse>> Execute(CancellationToken cancellationToken)
    {
        IReadOnlyList<Rifa> rifas = await rifaRepository.GetAllAsync(cancellationToken);

        return rifas.Select(rifa => new ListarRifasResponse
                    (
                        rifa.Id,
                        rifa.Nome,
                        rifa.Descricao,
                        rifa.ValorBilhete,
                        rifa.DataSorteio,
                        rifa.Premio,
                        rifa.Encerrada
                    ))
                    .ToList();
    }
}
