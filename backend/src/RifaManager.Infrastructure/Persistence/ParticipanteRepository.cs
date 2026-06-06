using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Enums;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class ParticipanteRepository(RifaDbContext context) : IParticipanteRepository
{
    public Task<Participante?> GetByIdAsync(Guid id)
    {
        return context.Participantes.FirstOrDefaultAsync(participante => participante.Id == id);
    }

    public Task<Participante?> GetByIdWithBilhetesAsync(Guid id)
    {
        return context.Participantes
            .Include(participante => participante.Bilhetes)
            .ThenInclude(bilhete => bilhete.Rifa)
            .FirstOrDefaultAsync(participante => participante.Id == id);
    }

    public async Task<IReadOnlyList<Participante>> GetByRifaIdAsync(Guid rifaId)
    {
        return await context.Participantes
            .AsNoTracking()
            .Where(participante => participante.Bilhetes.Any(bilhete => bilhete.RifaId == rifaId))
            .OrderBy(participante => participante.Nome)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Participante>> SearchAsync(string? nome, string? telefone, int? numeroBilhete, StatusPagamento? statusPagamento)
    {
        IQueryable<Participante> query = context.Participantes
            .AsNoTracking()
            .Include(participante => participante.Bilhetes)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(participante => participante.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(telefone))
            query = query.Where(participante => participante.Telefone.Contains(telefone));

        if (numeroBilhete.HasValue)
            query = query.Where(participante => participante.Bilhetes.Any(bilhete => bilhete.Numero == numeroBilhete.Value));

        if (statusPagamento.HasValue)
            query = query.Where(participante => participante.Bilhetes.Any(bilhete => bilhete.Status == statusPagamento.Value));

        return await query
            .OrderBy(participante => participante.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(Participante participante)
    {
        await context.Participantes.AddAsync(participante);
    }

    public Task UpdateAsync(Participante participante)
    {
        context.Participantes.Update(participante);
        return Task.CompletedTask;
    }
}
