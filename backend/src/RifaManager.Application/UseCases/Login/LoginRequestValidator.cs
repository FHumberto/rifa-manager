using FluentValidation;

namespace RifaManager.Application.UseCases.Login;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("O e-mail do usuário é obrigatório.")
            .EmailAddress();

        RuleFor(l => l.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
