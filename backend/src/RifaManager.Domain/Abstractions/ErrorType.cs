using System.ComponentModel;

namespace RifaManager.Domain.Abstractions;

public enum ErrorType
{
    [Description("Erro interno do servidor.")]
    Failure = 500,

    [Description("Erro de validacao.")]
    Validation = 400,

    [Description("Nao autorizado.")]
    AccessUnauthorized = 401,

    [Description("Acesso negado.")]
    AccessForbidden = 403,

    [Description("Recurso nao encontrado.")]
    NotFound = 404,

    [Description("Conflito.")]
    Conflict = 409
}
