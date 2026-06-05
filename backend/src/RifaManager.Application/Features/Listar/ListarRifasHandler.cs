using Ardalis.Result;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Domain.Entities;

namespace RifaManager.Application.Features.Listar;

public sealed class ListarRifasHandler(IRifaRepository rifaRepository) : IListarRifasUseCase
{
    public async Task<Result<IReadOnlyList<ListarRifasResponse>>> Execute()
    {
        IReadOnlyList<Rifa> rifas = await rifaRepository.GetAllAsync();

        IReadOnlyList<ListarRifasResponse> response = rifas
            .Select(rifa => new ListarRifasResponse(rifa.Id, rifa.Nome, rifa.ValorBilhete, rifa.Bilhetes.Count, rifa.DataSorteio, rifa.Encerrada))
            .ToList();

        return Result<IReadOnlyList<ListarRifasResponse>>.Success(response);
    }
}
