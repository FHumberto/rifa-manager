using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;

namespace RifaManager.Application.UseCases.Participantes.EditarParticipante;

public sealed class EditarParticipanteUseCaseHandler : IEditarParticipanteUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<EditarParticipanteRequest> _validator;
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditarParticipanteUseCaseHandler(IValidator<EditarParticipanteRequest> validator, IParticipanteRepository participanteRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _participanteRepository = participanteRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task Execute(Guid id, EditarParticipanteRequest request)
    {
        await ValidarRequisicao(request);

        Participante participante = await ValidarParticipante(id);

        participante.Atualizar(request.Nome, request.Telefone, request.Observacao);

        await _participanteRepository.UpdateAsync(participante);
        await _unitOfWork.CommitAsync();
    }

    private async Task<Participante> ValidarParticipante(Guid id)
    {
        Participante participante = await _participanteRepository.GetByIdWithBilhetesAsync(id)
            ?? throw new NotFoundException(ParticipanteErrors.ParticipanteNaoEncontrado.Description);

        if (participante.Bilhetes.Any(bilhete => bilhete.Rifa.Encerrada))
            throw new BadRequestException("Nao e possivel editar participante vinculado a rifa encerrada.");
        return participante;
    }

    private async Task ValidarRequisicao(EditarParticipanteRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
