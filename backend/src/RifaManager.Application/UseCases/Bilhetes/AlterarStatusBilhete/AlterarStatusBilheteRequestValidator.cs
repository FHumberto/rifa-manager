using FluentValidation;

namespace RifaManager.Application.UseCases.Bilhetes.AlterarStatusBilhete;

public sealed class AlterarStatusBilheteRequestValidator : AbstractValidator<AlterarStatusBilheteRequest>
{
    public AlterarStatusBilheteRequestValidator()
    {
        RuleFor(request => request.Status)
            .Must(status => Enum.IsDefined(status))
            .WithMessage("Status de pagamento invalido.");
    }
}
