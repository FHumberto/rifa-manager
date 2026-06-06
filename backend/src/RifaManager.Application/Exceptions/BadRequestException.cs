using FluentValidation.Results;

namespace RifaManager.Application.Exceptions;

public sealed class BadRequestException : Exception
{
    #region [ PROPRIEDADES ]

    public IReadOnlyDictionary<string, string[]>? ValidationErrors { get; }

    #endregion

    #region [ CONSTRUTORES ]

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(ValidationResult validationResult) => ValidationErrors = new Dictionary<string, string[]>(validationResult.ToDictionary());

    public BadRequestException() : base() { }

    #endregion
}
