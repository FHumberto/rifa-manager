using RifaManager.Domain.Entities;

namespace RifaManager.Domain.Persistence.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task AddAsync(Usuario usuario, CancellationToken cancellationToken);

    Task UpdateAsync(Usuario usuario);
}
