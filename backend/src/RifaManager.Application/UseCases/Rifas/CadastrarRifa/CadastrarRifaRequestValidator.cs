using FluentValidation;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.UseCases.Rifas.CadastrarRifa;

public sealed class CadastrarRifaRequestValidator : AbstractValidator<CadastrarRifaRequest>
{
    public CadastrarRifaRequestValidator()
    {
        RuleFor(rifa => rifa.Nome)
            .NotEmpty().WithMessage(RifaErrors.NomeObrigatorio.Description)
            .MaximumLength(100);

        RuleFor(rifa => rifa.Descricao)
            .NotEmpty().WithMessage(RifaErrors.DescricaoObrigatoria.Description)
            .MaximumLength(500);

        RuleFor(rifa => rifa.ValorBilhete)
            .GreaterThan(0).WithMessage(RifaErrors.ValorBilheteInvalido.Description);

        RuleFor(rifa => rifa.DataSorteio)
            .NotEmpty().WithMessage(RifaErrors.DataSorteioObrigatoria.Description)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage(RifaErrors.DataSorteioPassada.Description);

        RuleFor(rifa => rifa.Premio)
            .NotEmpty().WithMessage(RifaErrors.PremioObrigatorio.Description)
            .MaximumLength(200);
    }
}
