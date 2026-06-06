using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class BilheteRepository(RifaDbContext context) : IBilheteRepository
{
    public Task<Bilhete?> GetByIdAsync(Guid id)
    {
        return context.Bilhetes
            .AsNoTracking()
            .Include(bilhete => bilhete.Participante)
            .Include(bilhete => bilhete.UsuarioResponsavel)
            .FirstOrDefaultAsync(bilhete => bilhete.Id == id);
    }

    public async Task<IReadOnlyList<Bilhete>> GetByRifaIdAsync(Guid rifaId)
    {
        return await context.Bilhetes
            .AsNoTracking()
            .Include(bilhete => bilhete.Participante)
            .Include(bilhete => bilhete.UsuarioResponsavel)
            .Where(bilhete => bilhete.RifaId == rifaId)
            .OrderBy(bilhete => bilhete.Numero)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Bilhete>> GetByStatusAsync(StatusPagamento status, Guid? rifaId)
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
            .ToListAsync();
    }

    public async Task<int> GetMaiorNumeroByRifaIdAsync(Guid rifaId)
    {
        return await context.Bilhetes
            .Where(bilhete => bilhete.RifaId == rifaId)
            .Select(bilhete => (int?)bilhete.Numero)
            .MaxAsync() ?? 0;
    }

    public async Task AddRangeAsync(IEnumerable<Bilhete> bilhetes)
    {
        await context.Bilhetes.AddRangeAsync(bilhetes);
    }
}
