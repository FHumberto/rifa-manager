using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Usuarios.EditarUsuario;

public sealed class EditarUsuarioHandler : IEditarUsuarioUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<EditarUsuarioRequest> _validator;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarUsuarioHandler(IValidator<EditarUsuarioRequest> validator, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task Execute(Guid id, EditarUsuarioRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);

        Usuario usuario = await ValidarUsuario(id, request, cancellationToken);

        usuario.Atualizar(request.Nome, request.Email, request.Perfil);

        await _usuarioRepository.UpdateAsync(usuario);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<Usuario> ValidarUsuario(Guid id, EditarUsuarioRequest request, CancellationToken cancellationToken)
    {
        Usuario usuario = await _usuarioRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        Usuario? usuarioComMesmoEmail = await _usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (usuarioComMesmoEmail is not null && usuarioComMesmoEmail.Id != id)
            throw new BadRequestException(UsuarioErrors.EmailJaCadastrado.Description);

        return usuario;
    }

    private async Task ValidarRequisicao(EditarUsuarioRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
