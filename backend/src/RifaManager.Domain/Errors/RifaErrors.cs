using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Errors;

public static class RifaErrors
{
    public static readonly Error NomeObrigatorio =
        Error.Validation("Rifa.NomeObrigatorio", "O nome da rifa é obrigatório.");

    public static readonly Error DescricaoObrigatoria =
        Error.Validation("Rifa.DescricaoObrigatoria", "A descrição da rifa é obrigatória.");

    public static readonly Error ValorBilheteInvalido =
        Error.Validation("Rifa.ValorBilheteInvalido", "O valor do bilhete deve ser maior que zero.");

    public static readonly Error DataSorteioObrigatoria =
        Error.Validation("Rifa.DataSorteioObrigatoria", "A data do sorteio é obrigatória.");

    public static readonly Error DataSorteioPassada =
        Error.Validation("Rifa.DataSorteioPassada", "A data do sorteio deve ser no futuro.");

    public static readonly Error PremioObrigatorio =
        Error.Validation("Rifa.PremioObrigatorio", "O prêmio da rifa é obrigatório.");

    public static readonly Error JaEncerrada =
        Error.Conflict("Rifa.JaEncerrada", "A rifa já está encerrada.");

    public static readonly Error BilheteAlteracaoEmRifaEncerrada =
        Error.Conflict("Rifa.BilheteAlteracaoEmRifaEncerrada", "Não é possível alterar bilhetes de uma rifa encerrada.");

    public static readonly Error BilheteNaoPertenceARifa =
        Error.Validation("Rifa.BilheteNaoPertenceARifa", "O bilhete informado não pertence a esta rifa.");

    public static readonly Error SemBilhetesPagosParaSorteio =
        Error.Validation("Rifa.SemBilhetesPagosParaSorteio", "A rifa nao possui bilhetes pagos para sorteio.");

    public static readonly Error RifaNaoEncontrada =
        Error.NotFound("Rifa.NaoEncontrada", "Rifa nao encontrada.");
}
