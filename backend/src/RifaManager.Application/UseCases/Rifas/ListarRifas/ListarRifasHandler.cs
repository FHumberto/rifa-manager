using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Rifas.ListarRifas;

public sealed class ListarRifasHandler(IRifaRepository rifaRepository) : IListarRifasUseCase
{
    public async Task<IReadOnlyList<ListarRifasResponse>> Execute()
    {
        IReadOnlyList<Rifa> rifas = await rifaRepository.GetAllAsync();

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
