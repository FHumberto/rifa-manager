using RifaManager.Domain.Entities;

namespace RifaManager.Application.Abstractions.Persistence;

public interface IRifaRepository
{
    Task<Rifa?> GetByIdAsync(Guid id);

    Task AddAsync(Rifa rifa);
}
