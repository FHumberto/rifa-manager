using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence;

public interface IRifaRepository
{
    Task<Rifa?> GetByIdAsync(Guid id);

    Task<Rifa?> GetByIdWithBilhetesAsync(Guid id);

    Task<IReadOnlyList<Rifa>> GetAllAsync();

    Task AddAsync(Rifa rifa);

    Task UpdateAsync(Rifa rifa);
}
