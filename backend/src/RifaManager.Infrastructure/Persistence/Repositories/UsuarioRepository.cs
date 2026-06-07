using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence.Repositories;
using RifaManager.Infrastructure.Persistence.Context;

namespace RifaManager.Infrastructure.Persistence.Repositories;

internal sealed class UsuarioRepository(RifaDbContext context) : IUsuarioRepository
{
    public Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Id == id, cancellationToken);
    }

    public Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Email == email, cancellationToken);
    }

    public async Task AddAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        await context.Usuarios.AddAsync(usuario, cancellationToken);
    }

    public Task UpdateAsync(Usuario usuario)
    {
        context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }
}
