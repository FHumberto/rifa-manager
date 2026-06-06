namespace RifaManager.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    #region [ CONSTRUTORES ]

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

    public NotFoundException() : base() { }

    #endregion
}
