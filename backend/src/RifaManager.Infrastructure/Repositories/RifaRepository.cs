using Microsoft.EntityFrameworkCore;
using RifaManager.Application.Abstractions.Persistence;
using RifaManager.Domain.Entities;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Repositories;

public sealed class RifaRepository(RifaDbContext context) : IRifaRepository
{
    public Task<Rifa?> GetByIdAsync(Guid id)
    {
        return context.Rifas.AsNoTracking()
                            .Include(rifa => rifa.Bilhetes)
                            .FirstOrDefaultAsync(rifa => rifa.Id == id);
    }

    public async Task AddAsync(Rifa rifa)
    {
        await context.Rifas.AddAsync(rifa);
    }
}
