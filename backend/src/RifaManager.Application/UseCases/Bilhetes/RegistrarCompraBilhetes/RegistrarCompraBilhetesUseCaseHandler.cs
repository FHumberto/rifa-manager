using FluentValidation;
using FluentValidation.Results;
using RifaManager.Application.Exceptions;
using RifaManager.Domain.Entities;
using RifaManager.Domain.Errors;
using RifaManager.Domain.Persistence;
using RifaManager.Domain.Persistence.Repositories;

namespace RifaManager.Application.UseCases.Bilhetes.RegistrarCompraBilhetes;

public sealed class RegistrarCompraBilhetesUseCaseHandler : IRegistrarCompraBilhetesUseCase
{
    #region [ DEPENDÊNCIAS ]

    private readonly IValidator<RegistrarCompraBilhetesRequest> _validator;
    private readonly IRifaRepository _rifaRepository;
    private readonly IParticipanteRepository _participanteRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IBilheteRepository _bilheteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarCompraBilhetesUseCaseHandler(
        IValidator<RegistrarCompraBilhetesRequest> validator,
        IRifaRepository rifaRepository,
        IParticipanteRepository participanteRepository,
        IUsuarioRepository usuarioRepository,
        IBilheteRepository bilheteRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _rifaRepository = rifaRepository;
        _participanteRepository = participanteRepository;
        _usuarioRepository = usuarioRepository;
        _bilheteRepository = bilheteRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<RegistrarCompraBilhetesResponse> Execute(Guid usuarioResponsavelId, RegistrarCompraBilhetesRequest request, CancellationToken cancellationToken)
    {
        await ValidarRequisicao(request, cancellationToken);

        (Rifa rifa, Participante participante, Usuario usuarioResponsavel) = await ValidarDados(usuarioResponsavelId, request, cancellationToken);

        int maiorNumero = await _bilheteRepository.GetMaiorNumeroByRifaIdAsync(rifa.Id, cancellationToken);

        List<Bilhete> bilhetes = [];

        for (int index = 1; index <= request.Quantidade; index++)
        {
            bilhetes.Add(new Bilhete(maiorNumero + index, rifa, participante, usuarioResponsavel));
        }

        await _bilheteRepository.AddRangeAsync(bilhetes, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new RegistrarCompraBilhetesResponse
        (
            rifa.Id,
            participante.Id,
            bilhetes.Select(bilhete => new BilheteRegistradoResponse(bilhete.Id, bilhete.Numero)).ToList()
        );
    }

    private async Task<(Rifa rifa, Participante participante, Usuario usuarioResponsavel)> ValidarDados(Guid usuarioResponsavelId, RegistrarCompraBilhetesRequest request, CancellationToken cancellationToken)
    {
        Rifa rifa = await _rifaRepository.GetByIdAsync(request.RifaId, cancellationToken)
            ?? throw new NotFoundException(RifaErrors.RifaNaoEncontrada.Description);

        rifa.ValidarCompraDeBilhetes();

        Participante participante = await _participanteRepository.GetByIdAsync(request.ParticipanteId, cancellationToken)
            ?? throw new NotFoundException("Participante nao encontrado.");

        Usuario usuarioResponsavel = await _usuarioRepository.GetByIdAsync(usuarioResponsavelId, cancellationToken)
            ?? throw new NotFoundException("Usuario responsavel nao encontrado.");

        return (rifa, participante, usuarioResponsavel);
    }

    private async Task ValidarRequisicao(RegistrarCompraBilhetesRequest request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult);
    }
}
