namespace RifaManager.Domain.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
}
