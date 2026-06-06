using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Security.Cryptography;

namespace RifaManager.Application.UseCases.Usuarios.CadastrarUsuario;

public sealed class CadastrarUsuarioUseCaseHandler : ICadastrarUsuarioUseCase
{
    #region [ DEPENDENCIAS ]

    private readonly IValidator<CadastrarUsuarioRequest> _validator;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public CadastrarUsuarioUseCaseHandler(IValidator<CadastrarUsuarioRequest> validator, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter)
    {
        _validator = validator;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    #endregion

    public async Task<CadastrarUsuarioResponse> Execute(CadastrarUsuarioRequest request)
    {
        await ValidarRequisicao(request);
        await ValidarUsuario(request);

        string senhaHash = _passwordEncripter.Encrypt(request.Senha);
        Usuario usuario = new(request.Nome, request.Email, senhaHash, request.Perfil, true);

        await _usuarioRepository.AddAsync(usuario);
        await _unitOfWork.CommitAsync();

        return new CadastrarUsuarioResponse(usuario.Id);
    }

    private async Task ValidarUsuario(CadastrarUsuarioRequest request)
    {
        Usuario? usuarioExistente = await _usuarioRepository.GetByEmailAsync(request.Email);

        if (usuarioExistente is not null)
            throw new BadRequestException("E-mail ja cadastrado.");
    }

    private async Task ValidarRequisicao(CadastrarUsuarioRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
