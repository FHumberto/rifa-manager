namespace RifaManager.Application.Exceptions;

public sealed class UnauthorizedException : Exception
{
    #region [ CONSTRUTORES ]

    public UnauthorizedException(string message) : base(message) { }

    public UnauthorizedException() : base() { }

    #endregion
}
