using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Application.UseCases.EditarUsuario;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Usuarios.EditarUsuario;

public sealed class EditarUsuarioUseCaseHandler : IEditarUsuarioUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<EditarUsuarioRequest> _validator;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarUsuarioUseCaseHandler(IValidator<EditarUsuarioRequest> validator, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task Execute(Guid id, EditarUsuarioRequest request)
    {
        await ValidarRequisicao(request);

        Usuario usuario = await ValidarUsuario(id, request);

        usuario.Atualizar(request.Nome, request.Email, request.Perfil);

        await _usuarioRepository.UpdateAsync(usuario);
        await _unitOfWork.CommitAsync();
    }

    private async Task<Usuario> ValidarUsuario(Guid id, EditarUsuarioRequest request)
    {
        Usuario usuario = await _usuarioRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(UsuarioErrors.UsuarioNaoEncontrado.Description);

        Usuario? usuarioComMesmoEmail = await _usuarioRepository.GetByEmailAsync(request.Email);

        if (usuarioComMesmoEmail is not null && usuarioComMesmoEmail.Id != id)
            throw new BadRequestException(UsuarioErrors.EmailJaCadastrado.Description);

        return usuario;
    }

    private async Task ValidarRequisicao(EditarUsuarioRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
