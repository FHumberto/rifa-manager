using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class RifaRepository(RifaDbContext context) : IRifaRepository
{
    public Task<Rifa?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Rifas.FirstOrDefaultAsync(rifa => rifa.Id == id, cancellationToken);
    }

    public Task<Rifa?> GetByIdWithBilhetesAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Rifas
            .Include(rifa => rifa.Bilhetes)
            .ThenInclude(bilhete => bilhete.Participante)
            .FirstOrDefaultAsync(rifa => rifa.Id == id, cancellationToken);
    }

    public Task<Rifa?> GetByBilheteIdWithBilhetesAsync(Guid bilheteId, CancellationToken cancellationToken)
    {
        return context.Rifas
            .Include(rifa => rifa.Bilhetes)
            .FirstOrDefaultAsync(rifa => rifa.Bilhetes.Any(bilhete => bilhete.Id == bilheteId), cancellationToken);
    }

    public async Task<IReadOnlyList<Rifa>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Rifas
            .AsNoTracking()
            .OrderBy(rifa => rifa.DataSorteio)
            .ThenBy(rifa => rifa.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Rifa rifa, CancellationToken cancellationToken)
    {
        await context.Rifas.AddAsync(rifa, cancellationToken);
    }

    public Task UpdateAsync(Rifa rifa)
    {
        context.Rifas.Update(rifa);
        return Task.CompletedTask;
    }
}
