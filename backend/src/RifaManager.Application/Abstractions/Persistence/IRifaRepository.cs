using RifaManager.Domain.Entities;

namespace RifaManager.Application.Abstractions.Persistence;

public interface IRifaRepository
{
    Task<IReadOnlyList<Rifa>> GetAllAsync();

    Task<Rifa?> GetByIdAsync(Guid id);

    Task<Rifa?> GetByIdForUpdateAsync(Guid id);

    Task AddAsync(Rifa rifa);
}
