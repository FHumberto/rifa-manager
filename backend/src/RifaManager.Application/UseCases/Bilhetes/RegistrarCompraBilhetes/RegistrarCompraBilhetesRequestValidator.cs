using FluentValidation;

namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public sealed class RegistrarCompraBilhetesRequestValidator : AbstractValidator<RegistrarCompraBilhetesRequest>
{
    public RegistrarCompraBilhetesRequestValidator()
    {
        RuleFor(compra => compra.RifaId)
            .NotEmpty().WithMessage("A rifa e obrigatoria.");

        RuleFor(compra => compra.ParticipanteId)
            .NotEmpty().WithMessage("O participante e obrigatorio.");

        RuleFor(compra => compra.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade de bilhetes deve ser maior que zero.");
    }
}
