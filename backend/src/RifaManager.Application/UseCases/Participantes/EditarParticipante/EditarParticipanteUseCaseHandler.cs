using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

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

    public async Task Execute(Guid id, EditarParticipanteRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);

        Participante participante = await ValidarParticipante(id, cancellationToken);

        participante.Atualizar(request.Nome, request.Telefone, request.Observacao);

        await _participanteRepository.UpdateAsync(participante);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

    private async Task<Participante> ValidarParticipante(Guid id, CancellationToken cancellationToken)
    {
        Participante participante = await _participanteRepository.GetByIdWithBilhetesAsync(id, cancellationToken)
            ?? throw new NotFoundException(ParticipanteErrors.ParticipanteNaoEncontrado.Description);

        if (participante.Bilhetes.Any(bilhete => bilhete.Rifa.Encerrada))
            throw new BadRequestException(ParticipanteErrors.ParticipanteVinculadoRifaEncerrada.Description);
        return participante;
    }

    private async Task ValidarRequisicao(EditarParticipanteRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
