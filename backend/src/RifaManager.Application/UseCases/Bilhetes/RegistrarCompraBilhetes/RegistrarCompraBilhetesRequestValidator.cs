using FluentValidation;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public sealed class RegistrarCompraBilhetesRequestValidator : AbstractValidator<RegistrarCompraBilhetesRequest>
{
    public RegistrarCompraBilhetesRequestValidator()
    {
        RuleFor(compra => compra.RifaId)
            .NotEmpty().WithMessage(RifaErrors.RifaObrigatoria.Description);

        RuleFor(compra => compra.ParticipanteId)
            .NotEmpty().WithMessage(ParticipanteErrors.ParticipanteObrigatorio.Description);

        RuleFor(compra => compra.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade de bilhetes deve ser maior que zero.");
    }
}
