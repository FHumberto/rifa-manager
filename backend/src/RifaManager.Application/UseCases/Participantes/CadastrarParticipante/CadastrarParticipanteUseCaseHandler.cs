using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Participantes.CadastrarParticipante;

public sealed class CadastrarParticipanteUseCaseHandler : ICadastrarParticipanteUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<CadastrarParticipanteRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarParticipanteUseCaseHandler(IValidator<CadastrarParticipanteRequest> validator, IRifaRepository rifaRepository, IParticipanteRepository participanteRepository, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _participanteRepository = participanteRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<CadastrarParticipanteResponse> Execute(CadastrarParticipanteRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);
        await ValidarRifa(request, cancellationToken);

        Participante participante = new(request.Nome, request.Telefone, request.Observacao);

        await _participanteRepository.AddAsync(participante, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new CadastrarParticipanteResponse(participante.Id);
    }

    private async Task ValidarRifa(CadastrarParticipanteRequest request, CancellationToken cancellationToken)
    {
        Rifa rifa = await _rifaRepository.GetByIdAsync(request.RifaId, cancellationToken)
            ?? throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        rifa.ValidarCompraDeBilhetes();
    }

    private async Task ValidarRequisicao(CadastrarParticipanteRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
