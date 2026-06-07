using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence.Repositories;

public interface IRifaRepository
{
    Task<Rifa?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Rifa?> GetByIdWithBilhetesAsync(Guid id, CancellationToken cancellationToken);

    Task<Rifa?> GetByBilheteIdWithBilhetesAsync(Guid bilheteId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Rifa>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Rifa rifa, CancellationToken cancellationToken);

    Task UpdateAsync(Rifa rifa);
}
