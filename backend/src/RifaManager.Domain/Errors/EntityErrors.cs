using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Errors;

public static class EntityErrors
{
    public static readonly Error EntityIdInvalid = Error.Validation("Error.EntityIdInvalid", "O ID inválido.");
}
