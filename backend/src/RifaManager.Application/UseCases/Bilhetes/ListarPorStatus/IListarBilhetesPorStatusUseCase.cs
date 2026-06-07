using RifaManager.Application.Abstractions.Markers;
using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;

public interface IListarBilhetesPorStatusUseCase : IUseCase
{
    Task<IReadOnlyList<ListarBilhetesPorStatusResponse>> Execute(StatusPagamento status, Guid? rifaId, CancellationToken cancellationToken);
}
