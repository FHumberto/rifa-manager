using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Security.Cryptography;
using RifaManager.Domain.Security.Tokens;

namespace RifaManager.Application.UseCases.Login;

public sealed class LoginUseCaseHandler : ILoginUseCase
{
    #region [ DEPENDENCIAS ]

    private readonly IValidator<LoginRequest> _validator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginUseCaseHandler(IValidator<LoginRequest> validator, IUsuarioRepository usuarioRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _validator = validator;
        _usuarioRepository = usuarioRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    #endregion

    public async Task<LoginResponse> Execute(LoginRequest request)
    {
        await ValidarRequisicao(request);

        Usuario? usuario = await ValidarUsuario(request);

        string token = _accessTokenGenerator.Generate(usuario!);

        return new LoginResponse(token);
    }

    private async Task<Usuario?> ValidarUsuario(LoginRequest request)
    {
        Usuario? usuario = await _usuarioRepository.GetByEmailAsync(request.Email);

        if (usuario is null || !_passwordEncripter.IsValid(request.Senha, usuario.Senha))
            throw new UnauthorizedException(UsuarioErrors.UsuarioSenhaInvalida.Description);

        if (!usuario.PodeAcessarSistema())
            throw new UnauthorizedException(UsuarioErrors.InativoNaoPodeAcessarSistema.Description);

        return usuario;
    }

    private async Task ValidarRequisicao(LoginRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
