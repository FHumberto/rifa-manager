using FluentValidation;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.UseCases.Participantes.EditarParticipante;

public sealed class EditarParticipanteRequestValidator : AbstractValidator<EditarParticipanteRequest>
{
    public EditarParticipanteRequestValidator()
    {
        RuleFor(request => request.Nome)
            .NotEmpty().WithMessage(ParticipanteErrors.NomeObrigatorio.Description)
            .MaximumLength(100).WithMessage(ParticipanteErrors.TelefoneMuitoLongo.Description);

        RuleFor(request => request.Telefone)
            .NotEmpty().WithMessage(ParticipanteErrors.TelefoneObrigatorio.Description)
            .MaximumLength(20).WithMessage(ParticipanteErrors.TelefoneMuitoLongo.Description);
    }
}
