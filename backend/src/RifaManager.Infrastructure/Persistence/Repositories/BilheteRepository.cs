using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Persistence.Repositories;
using RifaManager.Infrastructure.Persistence.Context;

namespace RifaManager.Infrastructure.Persistence.Repositories;

internal sealed class BilheteRepository(RifaDbContext context) : IBilheteRepository
{
    public Task<Bilhete?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Bilhetes
            .AsNoTracking()
            .Include(bilhete => bilhete.Participante)
            .Include(bilhete => bilhete.UsuarioResponsavel)
            .FirstOrDefaultAsync(bilhete => bilhete.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Bilhete>> GetByRifaIdAsync(Guid rifaId, CancellationToken cancellationToken)
    {
        return await context.Bilhetes
            .AsNoTracking()
            .Include(bilhete => bilhete.Participante)
            .Include(bilhete => bilhete.UsuarioResponsavel)
            .Where(bilhete => bilhete.RifaId == rifaId)
            .OrderBy(bilhete => bilhete.Numero)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Bilhete>> GetByStatusAsync(StatusPagamento status, Guid? rifaId, CancellationToken cancellationToken)
    {
        IQueryable<Bilhete> query = context.Bilhetes
            .AsNoTracking()
            .Include(bilhete => bilhete.Participante)
            .Include(bilhete => bilhete.UsuarioResponsavel)
            .Where(bilhete => bilhete.Status == status);

        if (rifaId.HasValue)
        {
            query = query.Where(bilhete => bilhete.RifaId == rifaId.Value);
        }

        return await query
            .OrderBy(bilhete => bilhete.Numero)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetMaiorNumeroByRifaIdAsync(Guid rifaId, CancellationToken cancellationToken)
    {
        return await context.Bilhetes
            .Where(bilhete => bilhete.RifaId == rifaId)
            .Select(bilhete => (int?)bilhete.Numero)
            .MaxAsync(cancellationToken) ?? 0;
    }

    public async Task AddRangeAsync(IEnumerable<Bilhete> bilhetes, CancellationToken cancellationToken)
    {
        await context.Bilhetes.AddRangeAsync(bilhetes, cancellationToken);
    }
}
