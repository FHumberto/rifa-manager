using FluentValidation;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.UseCases.Participantes.CadastrarParticipante;

public sealed class CadastrarParticipanteRequestValidator : AbstractValidator<CadastrarParticipanteRequest>
{
    public CadastrarParticipanteRequestValidator()
    {
        RuleFor(request => request.RifaId)
            .NotEmpty().WithMessage(RifaErrors.RifaObrigatoria.Description);

        RuleFor(request => request.Nome)
            .NotEmpty().WithMessage(ParticipanteErrors.NomeObrigatorio.Description);

        RuleFor(request => request.Telefone)
            .NotEmpty().WithMessage(ParticipanteErrors.TelefoneObrigatorio.Description);
    }
}
