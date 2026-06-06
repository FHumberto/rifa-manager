using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Rifas.EditarRifa;

public sealed class EditarRifaUseCaseHandler : IEditarRifaUseCase
{
    #region [ DEENDÊNCIAS ]

    private readonly IValidator<EditarRifaRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarRifaUseCaseHandler(IValidator<EditarRifaRequest> validator, IRifaRepository rifaRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task Execute(Guid id, EditarRifaRequest request)
    {
        await ValidarRequisicao(request);

        Rifa rifa = await _rifaRepository.GetByIdAsync(id) ?? throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        rifa.Atualizar(request.Nome, request.Descricao, request.ValorBilhete, request.DataSorteio, request.Premio);

        await _rifaRepository.UpdateAsync(rifa);
        await _unitOfWork.CommitAsync();
    }

    private async Task ValidarRequisicao(EditarRifaRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
