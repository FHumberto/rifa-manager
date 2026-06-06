using RifaManager.Domain.Enums;

namespace RifaManager.Application.UseCases.Bilhetes.ListarPorStatus;

public interface IListarBilhetesPorStatusUseCase
{
    Task<IReadOnlyList<ListarBilhetesPorStatusResponse>> Execute(StatusPagamento status, Guid? rifaId);
}
