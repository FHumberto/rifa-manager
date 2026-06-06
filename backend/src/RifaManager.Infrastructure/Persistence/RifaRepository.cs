using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class RifaRepository(RifaDbContext context) : IRifaRepository
{
    public Task<Rifa?> GetByIdAsync(Guid id)
    {
        return context.Rifas.FirstOrDefaultAsync(rifa => rifa.Id == id);
    }

    public Task<Rifa?> GetByIdWithBilhetesAsync(Guid id)
    {
        return context.Rifas
            .Include(rifa => rifa.Bilhetes)
            .ThenInclude(bilhete => bilhete.Participante)
            .FirstOrDefaultAsync(rifa => rifa.Id == id);
    }

    public async Task<IReadOnlyList<Rifa>> GetAllAsync()
    {
        return await context.Rifas
            .AsNoTracking()
            .OrderBy(rifa => rifa.DataSorteio)
            .ThenBy(rifa => rifa.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(Rifa rifa)
    {
        await context.Rifas.AddAsync(rifa);
    }

    public Task UpdateAsync(Rifa rifa)
    {
        context.Rifas.Update(rifa);
        return Task.CompletedTask;
    }
}
