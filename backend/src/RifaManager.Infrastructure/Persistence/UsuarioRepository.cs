using Microsoft.EntityFrameworkCore;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class UsuarioRepository(RifaDbContext context) : IUsuarioRepository
{
    public Task<Usuario?> GetByIdAsync(Guid id)
    {
        return context.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
    }

    public Task<Usuario?> GetByEmailAsync(string email)
    {
        return context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Email == email);
    }

    public async Task AddAsync(Usuario usuario)
    {
        await context.Usuarios.AddAsync(usuario);
    }

    public Task UpdateAsync(Usuario usuario)
    {
        context.Usuarios.Update(usuario);
        return Task.CompletedTask;
    }
}
