using RifaManager.Domain.Abstractions;

namespace RifaManager.Domain.Errors;

public static class RifaErrors
{
    public static readonly Error NaoEncontrada =
        Error.NotFound("Rifa.NaoEncontrada", "Rifa nao encontrada.");

    public static readonly Error RequestObrigatorio =
        Error.Validation("Rifa.RequestObrigatorio", "Dados da rifa obrigatorios.");

    public static readonly Error NomeObrigatorio =
        Error.Validation("Rifa.NomeObrigatorio", "O nome da rifa e obrigatorio.");

    public static readonly Error NomeMuitoCurto =
        Error.Validation("Rifa.NomeMuitoCurto", "O nome da rifa deve conter pelo menos 4 caracteres.");

    public static readonly Error NomeMuitoLongo =
        Error.Validation("Rifa.NomeMuitoLongo", "O nome da rifa deve conter no maximo 100 caracteres.");

    public static readonly Error DescricaoObrigatoria =
        Error.Validation("Rifa.DescricaoObrigatoria", "A descricao da rifa e obrigatoria.");

    public static readonly Error DescricaoMuitoCurta =
        Error.Validation("Rifa.DescricaoMuitoCurta", "A descricao da rifa deve conter pelo menos 5 caracteres.");

    public static readonly Error DescricaoMuitoLonga =
        Error.Validation("Rifa.DescricaoMuitoLonga", "A descricao da rifa deve conter no maximo 500 caracteres.");

    public static readonly Error ValorBilheteInvalido =
        Error.Validation("Rifa.ValorBilheteInvalido", "O valor do bilhete deve ser maior que zero.");

    public static readonly Error DataSorteioObrigatoria =
        Error.Validation("Rifa.DataSorteioObrigatoria", "A data do sorteio e obrigatoria.");

    public static readonly Error DataSorteioPassada =
        Error.Validation("Rifa.DataSorteioPassada", "A data do sorteio deve ser no futuro.");

    public static readonly Error PremioObrigatorio =
        Error.Validation("Rifa.PremioObrigatorio", "O premio da rifa e obrigatorio.");

    public static readonly Error PremioMuitoCurto =
        Error.Validation("Rifa.PremioMuitoCurto", "O premio da rifa deve conter pelo menos 3 caracteres.");

    public static readonly Error PremioMuitoLongo =
        Error.Validation("Rifa.PremioMuitoLongo", "O premio da rifa deve conter no maximo 200 caracteres.");

    public static readonly Error JaEncerrada =
        Error.Conflict("Rifa.JaEncerrada", "A rifa ja esta encerrada.");

    public static readonly Error BilheteAlteracaoEmRifaEncerrada =
        Error.Conflict("Rifa.BilheteAlteracaoEmRifaEncerrada", "Nao e possivel alterar bilhetes de uma rifa encerrada.");

    public static readonly Error BilheteNaoPertenceARifa =
        Error.Validation("Rifa.BilheteNaoPertenceARifa", "O bilhete informado nao pertence a esta rifa.");
}
