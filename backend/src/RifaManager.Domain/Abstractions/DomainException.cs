namespace RifaManager.Domain.Abstractions;

public sealed class DomainException(Error error) : Exception(error.Description)
{
    public Error Error { get; } = error;
}
