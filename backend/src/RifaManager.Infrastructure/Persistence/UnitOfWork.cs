using RifaManager.Domain.Persistence;
using RifaManager.Infrastructure.Context;

namespace RifaManager.Infrastructure.Persistence;

internal sealed class UnitOfWork(RifaDbContext context) : IUnitOfWork
{
    public Task CommitAsync()
    {
        return context.SaveChangesAsync();
    }
}
