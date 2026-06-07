using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Persistence;

public interface IBilheteRepository
{
    Task<Bilhete?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Bilhete>> GetByRifaIdAsync(Guid rifaId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Bilhete>> GetByStatusAsync(StatusPagamento status, Guid? rifaId, CancellationToken cancellationToken);

    Task<int> GetMaiorNumeroByRifaIdAsync(Guid rifaId, CancellationToken cancellationToken);

    Task AddRangeAsync(IEnumerable<Bilhete> bilhetes, CancellationToken cancellationToken);
}
