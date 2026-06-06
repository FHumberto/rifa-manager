using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;

namespace RifaManager.Domain.Persistence;

public interface IBilheteRepository
{
    Task<Bilhete?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<Bilhete>> GetByRifaIdAsync(Guid rifaId);

    Task<IReadOnlyList<Bilhete>> GetByStatusAsync(StatusPagamento status, Guid? rifaId);

    Task<int> GetMaiorNumeroByRifaIdAsync(Guid rifaId);

    Task AddRangeAsync(IEnumerable<Bilhete> bilhetes);
}
