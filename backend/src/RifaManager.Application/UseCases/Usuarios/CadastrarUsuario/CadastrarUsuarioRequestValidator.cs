using FluentValidation;
using RifaManager.Domain.Errors;

namespace RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;

public sealed class CadastrarUsuarioRequestValidator : AbstractValidator<CadastrarUsuarioRequest>
{
    public CadastrarUsuarioRequestValidator()
    {
        RuleFor(usuario => usuario.Nome)
            .NotEmpty().WithMessage(UsuarioErrors.NomeObrigatorio.Description)
            .MaximumLength(100).WithMessage(UsuarioErrors.NomeMuitoLongo.Description);

        RuleFor(usuario => usuario.Email)
            .NotEmpty().WithMessage(UsuarioErrors.EmailObrigatorio.Description)
            .EmailAddress()
            .MaximumLength(100).WithMessage(UsuarioErrors.EmailMuitoLongo.Description);

        RuleFor(usuario => usuario.Senha)
            .NotEmpty().WithMessage(UsuarioErrors.SenhaObrigatoria.Description);

        RuleFor(usuario => usuario.Perfil)
            .Must(perfil => Enum.IsDefined(perfil))
            .WithMessage(UsuarioErrors.PerfilInvalido.Description);
    }
}
