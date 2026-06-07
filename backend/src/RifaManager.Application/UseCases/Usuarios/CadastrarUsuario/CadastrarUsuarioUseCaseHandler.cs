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

    public async Task<CadastrarUsuarioResponse> Execute(CadastrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);
        await ValidarUsuario(request, cancellationToken);

        string senhaHash = _passwordEncripter.Encrypt(request.Senha);
        Usuario usuario = new(request.Nome, request.Email, senhaHash, request.Perfil, true);

        await _usuarioRepository.AddAsync(usuario, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CadastrarUsuarioResponse(usuario.Id);
    }

    private async Task ValidarUsuario(CadastrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        Usuario? usuarioExistente = await _usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (usuarioExistente is not null)
            throw new BadRequestException("E-mail ja cadastrado.");
    }

    private async Task ValidarRequisicao(CadastrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
