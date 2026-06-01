namespace RifaManager.Domain.Abstractions;

public sealed class DomainException : Exception
{
    public Error Error { get; }

    public DomainException(Error error) : base(error.Description)
    {
        Error = error;
    }
}
